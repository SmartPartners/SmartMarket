using SmartMarket.Domin.Configurations;
using SmartMarket.Service.DTOs.Tolov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Interfrace.Tolov
{
    public interface ITolovService
    {

        public Task<bool> DeleteAsync(long id);

        public Task<IEnumerable<TolovForResultDto>> GetAllAsync();

        public Task<TolovForResultDto> GetByIdAsync(long id);

    }
}
