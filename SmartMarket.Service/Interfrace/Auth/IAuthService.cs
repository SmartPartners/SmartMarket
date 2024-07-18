using SmartMarket.Service.Commons.Response;
using SmartMarket.Service.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.Interfrace.Auth
{
    public interface IAuthService
    {
        public Task<BaseResponse<string>> LoginAsync(LoginDTO loginDto);
    }
}
