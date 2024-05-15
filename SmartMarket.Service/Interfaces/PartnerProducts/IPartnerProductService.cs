using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Enums;
using SmartMarket.Service.DTOs.PartnerProducts;
using SmartMarket.Service.DTOs.Partners;

namespace SmartMarket.Service.Interfaces.PartnerProducts;

public interface IPartnerProductService
{
    Task<bool> RemoveAsync(long id);
    string GenerateTransactionNumber();
    Task<PartnerProductForResultDto> RetrieveByIdAsync(long id);
    Task<PartnerProductForResultDto> RetrieveByTransNoAsync(string transNo);
    Task<PartnerProductForResultDto> ModifyAsync(long id, PartnerProductForUpdateDto dto);
    Task<IEnumerable<PartnerProductForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<PartnerForResultDto> PayForProductsAsync(long partnerId, decimal paid, TolovUsuli tolovUsuli);
    Task<PartnerProductForResultDto> CalculeteDiscountPercentageAsync(long id, short discountPercentage);
    Task<PartnerProductForResultDto> MoveProductToPartnerProductAsync(long id, long partnerId, long userId, decimal quantityToMove, string transNo);
    Task<IEnumerable<PartnerProductForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate);
}
