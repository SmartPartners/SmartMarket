using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartMarket.Data.IRepositories;
using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Entities.Partners;
using SmartMarket.Domin.Entities.Products;
using SmartMarket.Service.Commons.Exceptions;
using SmartMarket.Service.Commons.Extensions;
using SmartMarket.Service.DTOs.Cards;
using SmartMarket.Service.DTOs.PartnerProducts;
using SmartMarket.Service.DTOs.Partners;
using SmartMarket.Service.Interfaces.Categories;
using SmartMarket.Service.Interfaces.PartnerProducts;
using SmartMarket.Service.Interfaces.Partners;
using SmartMarket.Service.Interfaces.Users;

namespace SmartMarket.Service.Services.PartnerProducts;

public class PartnerProductService : IPartnerProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Partner> _partnerRepository;
    private readonly IRepository<PartnerProduct> _partnerProductRepository;
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;
    private readonly IUserService _userService;
    private readonly IPartnerService _partnerService;

    public PartnerProductService(IRepository<Product> productRepository, IRepository<Partner> partnerRepository, IRepository<PartnerProduct> partnerProductRepository, IMapper mapper, ICategoryService categoryService, IUserService userService, IPartnerService partnerService)
    {
        _productRepository = productRepository;
        _partnerRepository = partnerRepository;
        _partnerProductRepository = partnerProductRepository;
        _mapper = mapper;
        _categoryService = categoryService;
        _userService = userService;
        _partnerService = partnerService;
    }

    public async Task<PartnerProductForResultDto> MoveProductToPartnerProductAsync(long id, long partnerId, long userId, decimal quantityToMove, string transNo)
    {
        var insufficientProduct = await _productRepository.SelectAll()
            .Where(p => p.Id == id && p.Quantity < quantityToMove)
            .FirstOrDefaultAsync();
        if (insufficientProduct is not null)
            throw new CustomException(400, $"Omborda buncha mahsulot mavjud emas.\nOmborda {insufficientProduct.Quantity} ta mahsulot bor.");

        if (quantityToMove == 0)
            throw new CustomException(404, "Mahsulotni hajmini kiriting.");

        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (product == null || product.Quantity == 0)
            throw new CustomException(404, "Bu turdagi mahsulot omborda mavjud emas.");

        var partnerProduct = new PartnerProduct
        {
            PartnerId = partnerId,  
            CategoryId = product.CategoryId,
            UserId = userId,
            ProductName = product.Name,
            TransNo = transNo,
            PCode = product.PCode,
            BarCode = product.BarCode,
            Price = product.SalePrice ?? 0,
            Quantity = quantityToMove,
            OlchovBirligi = product.OlchovTuri,
            CreatedAt = DateTime.UtcNow,
        };
        partnerProduct.TotalPrice = partnerProduct.Price * partnerProduct.Quantity;

        var partnerDebt = await _partnerRepository.SelectAll()
            .Where(p => p.Id == partnerProduct.PartnerId)
            .FirstOrDefaultAsync();
        partnerDebt.Debt += partnerProduct.TotalPrice;
        partnerDebt.UpdatedAt = DateTime.UtcNow;
        await _partnerRepository.UpdateAsync(partnerDebt);

        var partnerProducts = await _partnerProductRepository.SelectAll()
            .Where(p => p.PCode == product.PCode && p.BarCode == product.BarCode)
            .FirstOrDefaultAsync();
        if (partnerProducts is not null)
        {
            partnerProducts.Quantity += quantityToMove;
            partnerProducts.UpdatedAt = DateTime.UtcNow;
            await _partnerProductRepository.UpdateAsync(partnerProducts);

            product.Quantity -= quantityToMove;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
        }
        else
        {
        await _partnerProductRepository.InsertAsync(partnerProduct);
        }

        product.Quantity -= quantityToMove;
        product.UpdatedAt = DateTime.UtcNow;
        await _productRepository.UpdateAsync(product);

        return _mapper.Map<PartnerProductForResultDto>(partnerProduct);
    }

    public async Task<PartnerForResultDto> PayForProductsAsync(long partnerId, decimal paid)
    {
        var partnerProduct = await _partnerProductRepository.SelectAll()
            .Where(p => p.PartnerId == partnerId)
            .FirstOrDefaultAsync();
        if (partnerProduct is not null)
        {
            var partnerDebt = await _partnerRepository.SelectAll()
            .Where(p => p.Id == partnerProduct.PartnerId)
            .FirstOrDefaultAsync();
            if (partnerDebt.Debt > 0)
            {
                var nat = partnerDebt.Debt -= paid;
                partnerDebt.Debt = nat;
                partnerDebt.Paid = paid;
                partnerProduct.UpdatedAt = DateTime.UtcNow;
                await _partnerRepository.UpdateAsync(partnerDebt);

                if (partnerDebt.Debt == 0)
                {
                    partnerDebt.Paid = 0;
                    partnerDebt.UpdatedAt = new DateTime(0001, 1, 1);
                }
            }
            else
            {
                throw new CustomException(400, "Qarz qolmadi.");
            }
        }
        return _mapper.Map<PartnerForResultDto>(partnerProduct);
    }

    public async Task<PartnerProductForResultDto> CalculeteDiscountPercentageAsync(long id, short discountPercentage)
    {
        var card = await _partnerProductRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (card is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        decimal discountAmount = (card.TotalPrice / 100) * discountPercentage;

        decimal discountedTotalPrice = card.TotalPrice - discountAmount;

        card.TotalPrice = discountedTotalPrice;
        card.DiscountPrice = discountAmount;

        await _partnerProductRepository.UpdateAsync(card);

        return _mapper.Map<PartnerProductForResultDto>(card);
    }

    public async Task<PartnerProductForResultDto> ModifyAsync(long id, PartnerProductForUpdateDto dto)
    {
        var category = await _categoryService.RetrieveByIdAsync(dto.CategoryId);

        var user = await _userService.RetrieveByIdAsync(dto.UserId);

        var partner = await _partnerService.RetrieveByIdAsync(dto.PartnerId);

        var partnerProduct = await _partnerProductRepository.SelectAll()
            .Where(s => s.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (partner is null)
            throw new CustomException(404, "Bu mahsulot topilmadi.");

        var mappedStockProduct = _mapper.Map(dto, partnerProduct);
        mappedStockProduct.UpdatedAt = DateTime.UtcNow;

        var result = await _partnerProductRepository.UpdateAsync(mappedStockProduct);

        return _mapper.Map<PartnerProductForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var StockProduct = await _partnerProductRepository.SelectAll()
            .Where(s => s.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (StockProduct is null)
            throw new CustomException(404, "Bu mahsulot topilmadi.");

        return await _partnerProductRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<PartnerProductForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var partnerProducts = await _partnerProductRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PartnerProductForResultDto>>(partnerProducts);
    }

    public async Task<IEnumerable<PartnerProductForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate)
    {
        if (userId != null)
        {
            var product = await _partnerProductRepository.SelectAll()
                .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate && p.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        var products = await _partnerProductRepository.SelectAll()
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<PartnerProductForResultDto>>(products);
    }

    public async Task<PartnerProductForResultDto> RetrieveByIdAsync(long id)
    {
        var StockProduct = await _partnerProductRepository.SelectAll()
          .Where(s => s.Id == id)
          .AsNoTracking()
          .FirstOrDefaultAsync();

        if (StockProduct is null)
            throw new CustomException(404, "Bu mahsulot topilmadi.");

        return _mapper.Map<PartnerProductForResultDto>(StockProduct);
    }

    private static int lastTransactionNumberSuffix = 2001;
    private static DateTime lastTransactionDate = DateTime.UtcNow.Date;

    public string GenerateTransactionNumber()
    {
        // Use current date in "yyyyMMdd" format
        DateTime currentDate = DateTime.UtcNow.Date;

        // Check if it's a new day
        if (currentDate > lastTransactionDate)
        {
            // Reset the suffix to 2001 for the new day
            lastTransactionNumberSuffix = 2001;
            lastTransactionDate = currentDate;
        }

        string transactionNumber;
        do
        {
            transactionNumber = currentDate.ToString("yyyyMMdd") + lastTransactionNumberSuffix.ToString();
            lastTransactionNumberSuffix++;
        } while (_partnerProductRepository.SelectAll().Any(t => t.TransNo == transactionNumber));

        return transactionNumber;
    }

    public async Task<PartnerProductForResultDto> RetrieveByTransNoAsync(string transNo)
    {
        var partnerProduct = await _partnerProductRepository.SelectAll()
          .Where(s => s.TransNo == transNo)
          .AsNoTracking()
          .FirstOrDefaultAsync();

        if (partnerProduct is null)
            throw new CustomException(404, "Bu mahsulot topilmadi.");

        return _mapper.Map<PartnerProductForResultDto>(partnerProduct);
    }
}
