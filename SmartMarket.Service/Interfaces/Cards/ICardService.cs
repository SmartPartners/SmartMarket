using SmartMarket.Domin.Configurations;
using SmartMarket.Service.DTOs.Cards;

namespace SmartMarket.Service.Interfaces.Cards;

public interface ICardService
{
    Task<bool> ReamoveAsync(long id);
    string GenerateTransactionNumber();
    Task<CardForResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<CardForResultDto>> SvetUchgandaAsync(string status);
    Task<IEnumerable<CardForResultDto>> GetByBarCodeAsync(string barCode);
    Task<IEnumerable<CardForResultDto>> RetrieveAllWithMaxSaledAsync(int takeMax);
    Task<IEnumerable<CardForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<CardForResultDto> InsertWithBarCodeAsync(string barCode, decimal quantity);
    Task<CardForResultDto> UpdateWithTransactionNumberAsync(string transactionNumber);
    Task<CardForResultDto> CalculeteDiscountPercentageAsync(long id, short discountPercentage);
    Task<CardForResultDto> MoveProductToCardAsync(long id, long userId, decimal quantityToMove, string trnasNo);
    Task<IEnumerable<CardForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate);
    Task<CardForResultDto> SaleProductWithBarCodeAsync(string barCode, long userId, decimal quantityToMove, string trnasNo);
}
