using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Enums;
using SmartMarket.Service.DTOs.Cards;

namespace SmartMarket.Service.Interfaces.Cards;

public interface ICardService
{
    Task<bool> ReamoveAsync(long id);
    string GenerateTransactionNumber();
    Task<CardForResultDto> RetrieveByIdAsync(long id);
    Task<CardForResultDto> GetByBarCodeAsync(string barCode);
    Task<IEnumerable<CardForResultDto>> SvetUchgandaAsync(string status);
    Task<IEnumerable<CardForResultDto>> RetrieveAllWithMaxSaledAsync(DateTime startDate, DateTime endDate, int takeMax);
    Task<IEnumerable<CardForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<CardForResultDto> UpdateWithTransactionNumberAsync(string transactionNumber, TolovUsuli tolovUsuli);
    Task<CardForResultDto> CalculeteDiscountPercentageAsync(long id, short discountPercentage);
    Task<bool> CancelProductAtListByPlanshetAsync(string transNo, string barCode, decimal quantity);
    Task<CardForResultDto> MoveProductToCardAsync(long id, long? yukYiguvchId, long userId, decimal quantityToMove, string transNo);
    //Task<bool> CancelProductsAtListByPlanshetAsync(List<(string transNo, string barCode, decimal quantity)> productList);
    Task<CardForResultDto> SaleProductWithBarCodeAsync(string barCode, long? yukYiguvchId, long userId, decimal quantityToMove, string transNo);
    Task<IEnumerable<CardForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate);
}
