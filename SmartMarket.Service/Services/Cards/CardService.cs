using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartMarket.Data.IRepositories;
using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Entities.Cards;
using SmartMarket.Domin.Entities.Products;
using SmartMarket.Service.Commons.Exceptions;
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
        card.DiscountPrice = (short)discountAmount;

        await _cardRepository.UpdateAsync(card);

        return _mapper.Map<CardForResultDto>(card);
    }


    public async Task<IEnumerable<CardForResultDto>> GetByBarCodeAsync(string barCode)
    {
        var codes = await _cardRepository.SelectAll()
            .Where(c => c.BarCode == barCode)
            .ToListAsync();
        if (codes is null)
            throw new CustomException(404, "Mahsulot topilmadi.");


        return _mapper.Map<IEnumerable<CardForResultDto>>(codes);
    }

    public async Task<CardForResultDto> InsertWithBarCodeAsync(string barCode, decimal quantity)
    {
        var card = await _cardRepository.SelectAll()
             .Where(p => p.BarCode == barCode)
             .AsNoTracking()
             .FirstOrDefaultAsync();


        var mappedProduct = new Card
        {
            CasherId = card.CasherId,
            ProductName = card.ProductName,
            TransNo = card.TransNo,
            BarCode = card.BarCode,
            Price = card.Price,
            TotalPrice = card.TotalPrice,
            DiscountPrice = card.DiscountPrice,
            Status = card.Status,
            Quantity = card.Quantity,
            SalePrice = card.SalePrice,
            PercentageOfPrice = card.PercentageOfPrice,
            CreatedAt = DateTime.UtcNow
        };

        var product = await _productRepository.SelectAll()
            .Where(s => s.BarCode == mappedProduct.BarCode)
            .FirstOrDefaultAsync();
        if (product != null)
        {
            product.Quantity -= quantity;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
        }

        card.Quantity += quantity;
        card.UpdatedAt = DateTime.UtcNow;
        await _cardRepository.UpdateAsync(card);

        return _mapper.Map<CardForResultDto>(mappedProduct);
    }

    public async Task<CardForResultDto> MoveProductToCardAsync(long id, long userId, decimal quantityToMove, string trnasNo)
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
            CasherId = userId,
            BarCode = product.BarCode,
            ProductName = product.Name,
            Price = product.SalePrice ?? 0,
            Quantity = quantityToMove,
            Status = "Kutilmoqda",
            TransNo = trnasNo,
            CreatedAt = DateTime.UtcNow
        };
        card.TotalPrice = card.Price * card.Quantity;

        var existingCard = await _cardRepository.SelectAll()
            .Where(p => p.TransNo == card.TransNo && p.BarCode == card.BarCode)
            .FirstOrDefaultAsync();

        if (existingCard != null)
        {
            existingCard.Quantity += quantityToMove;
            existingCard.TotalPrice = existingCard.SalePrice * existingCard.Quantity ?? 0;
            existingCard.UpdatedAt = DateTime.UtcNow;
            await _cardRepository.UpdateAsync(existingCard);
        }
        else
        {
            await _cardRepository.InsertAsync(card);
        }

        return _mapper.Map<CardForResultDto>(card);
    }

    public Task<bool> ReamoveAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CardForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CardForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CardForResultDto>> RetrieveAllWithMaxSaledAsync(int takeMax)
    {
        throw new NotImplementedException();
    }

    public Task<CardForResultDto> RetrieveByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<CardForResultDto> SaleProductWithBarCodeAsync(string barCode, long userId, decimal quantityToMove, string trnasNo)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CardForResultDto>> SvetUchgandaAsync(string status)
    {
        throw new NotImplementedException();
    }

    public Task<CardForResultDto> UpdateWithTransactionNumberAsync(string transactionNumber)
    {
        throw new NotImplementedException();
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
}
