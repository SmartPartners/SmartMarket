using SmartMarket.Domin.Configurations;
using SmartMarket.Service.DTOs.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Interfrace.Partners
{
    public interface IPartnerService
    {
        public Task<PartnerForResultDto> CreateAsync(PartnerForCreationDto entity);

        public Task<PartnerForResultDto> ModifyAsync(long id, PartnerForUpdateDto dto);

        public Task<bool> DeleteAsync(long id);

        public Task<IEnumerable<PartnerForResultDto>> GetAllAsync();

        public Task<PartnerForResultDto> GetByIdAsync(long id);
    }
}
