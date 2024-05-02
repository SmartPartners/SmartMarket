using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartMarket.Data.IRepositories;
using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Entities.Cards;
using SmartMarket.Domin.Entities.Products;
using SmartMarket.Service.Commons.Exceptions;
using SmartMarket.Service.Commons.Extensions;
using SmartMarket.Service.DTOs.Cards;
using SmartMarket.Service.Interfaces.Cards;

namespace SmartMarket.Service.Services.Cards;

public class CardService : ICardService
{
    private readonly IRepository<Card> _cardRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public CardService(IRepository<Card> cardRepository, IMapper mapper, IRepository<Product> productRepository)
    {
        _cardRepository = cardRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }


    public async Task<CardForResultDto> MoveProductToCardAsync(long id, long userId, decimal quantityToMove, string transNo)
    {
        var insufficientProduct = await _productRepository.SelectAll()
            .Where(q => q.Id == id && q.Quantity < quantityToMove)
            .FirstOrDefaultAsync();

        if (insufficientProduct != null)
            throw new CustomException(400, $"Magazinda buncha mahsulot mavjud emas.\nHozirda {insufficientProduct.Quantity} ta mahsulot bor.");

        if (quantityToMove <= 0)
            quantityToMove = 1;

        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (product == null)
            throw new CustomException(404, "Mahsulot topilmadi.");
        else if (product.Quantity == 0)
            throw new CustomException(404, "Hozirda bu mahsulotimiz tugagan.");

        var card = new Card
        {
            TransNo = transNo,
            ProductName = product.Name,
            BarCode = product.BarCode,
            CategoryId = product.CategoryId,
            CamePrice = product.CamePrice,
            SalePrice = product.SalePrice,
            PercentageOfPrice = product.PercentageOfPrice,
            Quantity = quantityToMove,
            OlchovBirligi = product.OlchovTuri,
            CasherId = userId,
            Status = "Kutilmoqda",
            CreatedAt = DateTime.UtcNow
        };
        card.TotalPrice = (card.SalePrice ?? 0) * card.Quantity;

        var existingCard = await _cardRepository.SelectAll()
            .Where(p => p.TransNo == card.TransNo && p.BarCode == card.BarCode)
            .FirstOrDefaultAsync();

        if (existingCard != null)
        {
            existingCard.Quantity += quantityToMove;
            existingCard.TotalPrice = existingCard.SalePrice * existingCard.Quantity ?? 0;
            existingCard.UpdatedAt = DateTime.UtcNow;
            await _cardRepository.UpdateAsync(existingCard);

            product.Quantity -= card.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
        }
        else
        {
            await _cardRepository.InsertAsync(card);

            product.Quantity -= card.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
        }

        return _mapper.Map<CardForResultDto>(card);
    }

    public async Task<CardForResultDto> SaleProductWithBarCodeAsync(string barCode, long userId, decimal quantityToMove, string transNo)
    {
        var insufficientProduct = await _productRepository.SelectAll()
            .Where(q => q.BarCode == barCode && q.Quantity < quantityToMove)
            .FirstOrDefaultAsync();

        if (insufficientProduct != null)
            throw new CustomException(400, $"Magazinda buncha mahsulot mavjud emas.\nHozirda {insufficientProduct.Quantity} ta mahsulot bor.");

        if (quantityToMove <= 0)
            quantityToMove = 1;

        var product = await _productRepository.SelectAll()
            .Where(p => p.BarCode == barCode)
            .FirstOrDefaultAsync();

        if (product == null)
            throw new CustomException(404, "Mahsulot topilmadi.");
        else if (product.Quantity == 0)
            throw new CustomException(404, "Hozirda bu mahsulotimiz tugagan.");

        var card = new Card
        {
            TransNo = transNo,
            ProductName = product.Name,
            BarCode = product.BarCode,
            CategoryId = product.CategoryId,
            CamePrice = product.CamePrice,
            SalePrice = product.SalePrice,
            PercentageOfPrice = product.PercentageOfPrice,
            Quantity = quantityToMove,
            OlchovBirligi = product.OlchovTuri,
            CasherId = userId,
            Status = "Kutilmoqda",
            CreatedAt = DateTime.UtcNow
        };
        card.TotalPrice = (card.SalePrice ?? 0) * card.Quantity;

        var existingCard = await _cardRepository.SelectAll()
            .Where(p => p.TransNo == card.TransNo && p.BarCode == card.BarCode)
            .FirstOrDefaultAsync();

        if (existingCard != null)
        {
            existingCard.Quantity += quantityToMove;
            existingCard.TotalPrice = existingCard.SalePrice * existingCard.Quantity ?? 0;
            existingCard.UpdatedAt = DateTime.UtcNow;
            await _cardRepository.UpdateAsync(existingCard);


            product.Quantity -= card.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
        }
        else
        {
            await _cardRepository.InsertAsync(card);

            product.Quantity -= card.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
        }

        return _mapper.Map<CardForResultDto>(card);
    }

    public async Task<CardForResultDto> CalculeteDiscountPercentageAsync(long id, short discountPercentage)
    {
        var card = await _cardRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (card is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        decimal discountAmount = (card.TotalPrice / 100) * discountPercentage;

        decimal discountedTotalPrice = card.TotalPrice - discountAmount;

        card.TotalPrice = discountedTotalPrice;
        card.DiscountPrice = discountAmount;

        await _cardRepository.UpdateAsync(card);

        return _mapper.Map<CardForResultDto>(card);
    }

    public async Task<CardForResultDto> UpdateWithTransactionNumberAsync(string transactionNumber)
    {
        var cards = await _cardRepository.SelectAll()
            .Where(t => t.TransNo == transactionNumber)
            .ToListAsync();

        foreach (var card in cards)
        {
            if (card != null)
            {
                card.Status = "Sotildi";
                card.UpdatedAt = DateTime.UtcNow;
                await _cardRepository.UpdateAsync(card);
            }
        }

        return _mapper.Map<CardForResultDto>(cards);
    }

    public async Task<CardForResultDto> GetByBarCodeAsync(string barCode)
    {
        var code = await _cardRepository.SelectAll()
            .Where(c => c.BarCode == barCode)
            .ToListAsync();
        if (code is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        return _mapper.Map<CardForResultDto>(code);
    }


    public async Task<bool> ReamoveAsync(long id)
    {
        var userlanguage = await _cardRepository.SelectAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        if (userlanguage is null)
            throw new CustomException(404, "Karta topilmadi.");

        await _cardRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<CardForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var cards = await _cardRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CardForResultDto>>(cards);
    }

    public async Task<CardForResultDto> RetrieveByIdAsync(long id)
    {
        var card = await _cardRepository.SelectAll()
           .Where(c => c.Id == id)
           .AsNoTracking()
           .FirstOrDefaultAsync();
        if (card is null)
            throw new CustomException(404, "Karta topilmadi.");

        return _mapper.Map<CardForResultDto>(card);
    }

    public async Task<IEnumerable<CardForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate)
    {
        if (userId != null)
        {
            var product = await _cardRepository.SelectAll()
                .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate && p.CasherId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        var products = await _cardRepository.SelectAll()
            .Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<CardForResultDto>>(products);
    }

    public async Task<IEnumerable<CardForResultDto>> RetrieveAllWithMaxSaledAsync(int takeMax)
    {
        var result = await _cardRepository.SelectAll()
            .Where(c => c.Status == "Sotildi")
            .OrderByDescending(c => c.Quantity)
            .Take(takeMax)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<CardForResultDto>>(result);
    }

    public async Task<IEnumerable<CardForResultDto>> SvetUchgandaAsync(string status)
    {
        var result = await _cardRepository.SelectAll()
            .Where(c => c.Status.ToLower() == status.ToLower())
            .AsNoTracking()
            .ToListAsync();
        if (result is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        return _mapper.Map<IEnumerable<CardForResultDto>>(result);
    }



    private static int lastTransactionNumberSuffix = 1000;
    private static DateTime lastTransactionDate = DateTime.UtcNow.Date;

    public string GenerateTransactionNumber()
    {
        DateTime currentDate = DateTime.UtcNow.Date;

        if (currentDate > lastTransactionDate)
        {
            lastTransactionNumberSuffix = 1001;
            lastTransactionDate = currentDate;
        }

        string transactionNumber;
        do
        {
            transactionNumber = currentDate.ToString("yyyyMMdd") + lastTransactionNumberSuffix.ToString();
            lastTransactionNumberSuffix++;
        } while (_cardRepository.SelectAll().Any(t => t.TransNo == transactionNumber));

        return transactionNumber;
    }


    /// <summary>
    /// Planshetdan bitta bitta cancel qilingan product uchun
    /// </summary>
    /// <param name="transNo"></param>
    /// <param name="barCode"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>

    public async Task<bool> CancelProductAtListByPlanshetAsync(string transNo, string barCode, decimal quantity)
    {
        var card = await _cardRepository.SelectAll()
            .Where(t => t.TransNo.ToLower() == transNo.ToLower())
            .FirstOrDefaultAsync();
        if (card is null)
            throw new CustomException(404, "Kard topilmadi.");

        if (quantity <= 0)
            quantity = 0;

        var product = await _productRepository.SelectAll()
            .Where(i => i.BarCode == barCode)
            .FirstOrDefaultAsync();
        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        product.Quantity += quantity;
        product.TotalPrice = (product.SalePrice ?? 0) * product.Quantity;
        await _productRepository.UpdateAsync(product);

        return true;
    }


    /// <summary>
    /// Planshetdan bir vaqtda hamma mahsulotlarni cancel qilish uchun
    /// </summary>
    /// <param name="productList"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    /*public async Task<bool> CancelProductsAtListByPlanshetAsync(List<(string transNo, string barCode, decimal quantity)> productList)
    {
        foreach (var (transNo, barCode, originalQuantity) in productList)
        {
            var card = await _cardRepository.SelectAll()
                .Where(t => t.TransNo.ToLower() == transNo.ToLower())
                .FirstOrDefaultAsync();
            if (card is null)
                throw new CustomException(404, "Kard topilmadi.");

            decimal quantity = originalQuantity;
            if (quantity <= 0)
                quantity = 0;

            var product = await _productRepository.SelectAll()
                .Where(i => i.BarCode == barCode)
                .FirstOrDefaultAsync();
            if (product is null)
                throw new CustomException(404, "Mahsulot topilmadi.");

            product.Quantity += quantity;
            product.TotalPrice = (product.SalePrice ?? 0) * product.Quantity;
            await _productRepository.UpdateAsync(product);
        }
        return true;
    }*/
}
