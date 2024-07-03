using SmartMarket.Domin.Configurations;
using SmartMarket.Service.DTOs.CancelOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Interfrace.CancelOrders
{
    public interface ICancelOrderService
    {

        public Task<CancelOrderForResultDto> CanceledProductsAsync(long id, decimal quantity, long canceledBy, string reason, bool action);

        public Task<CancelOrderForResultDto> CanceledProductsFromPArterAsync(long id, long partnerId, decimal quantity, long canceledBy, string reason, bool action);

        public Task<IEnumerable<CancelOrderForResultDto>> getAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate);

        public Task<IEnumerable<CancelOrderForResultDto>> YaroqsizMahsulotlarAsync(DateTime startDate, DateTime endDate);

        public Task<bool> DeleteAsync(long id);

        public Task<IEnumerable<CancelOrderForResultDto>> GetAllAsync();

        public Task<CancelOrderForResultDto> GetByIdAsync(long id);

    }
}
