using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Enums;
using SmartMarket.Service.DTOs.PartnerProducts;
using SmartMarket.Service.DTOs.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Interfrace.Products
{
    public interface IPartnerProductsService
    {
        public Task<PartnerProductForResultDto> MoveProductToPartnerProductAsync(long id, long partnerId, long userId, decimal quantityToMove, string transNo);

        public Task<PartnerForResultDto> PayForProductsAsync(long partnerId, decimal paid, TolovUsuli tolovUsuli);

        public Task<PartnerProductForResultDto> CalculeteDiscountPercentageAsync(long id, short discountPercentage);

        public Task<PartnerProductForResultDto> ModifyAsync(long id, PartnerProductForUpdateDto dto);

        public Task<bool> DeleteAsync(long id);

        public Task<IEnumerable<PartnerProductForResultDto>> GetAllAsync();

        public Task<IEnumerable<PartnerProductForResultDto>> GetAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate);

        public Task<PartnerProductForResultDto> GetByIdAsync(long id);

        public Task<string> GenerateTransactionNumber();

        public Task<PartnerProductForResultDto> GetByTransNoAsync(string transNo);
    }
}
