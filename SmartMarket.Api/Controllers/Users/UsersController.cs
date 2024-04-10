using Microsoft.AspNetCore.Mvc;
using SmartMarket.Api.Controllers.Commons;
using SmartMarket.Domin.Configurations;
using SmartMarket.Service.DTOs.Users;
using SmartMarket.Service.Interfaces.Users;

namespace SmartMarket.Api.Controllers.Users
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromForm] UserForCreationDto dto)
            => Ok(await _userService.CreateAsync(dto));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _userService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _userService.RetrieveByIdAsync(id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
            => Ok(await _userService.RemoveAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromForm] UserForUpdateDto dto)
            => Ok(await _userService.ModifyAsync(id, dto));

        [HttpPut("change-password")]
        public async Task<ActionResult<UserForResultDto>> ChangePasswordAsync(long id, [FromForm] UserForChangePasswordDto dto)
            => Ok(await _userService.ChangePasswordAsync(id, dto));
    }
}
