using Microsoft.AspNetCore.Mvc;
using SmartMarket.Api.Controllers.Commons;
using SmartMarket.Api.Models;
using SmartMarket.Domin.Configurations;
using SmartMarket.Service.DTOs.PartnerProducts;
using SmartMarket.Service.Interfaces.PartnerProducts;

namespace SmartMarket.Api.Controllers.PartnerProduct
{
    public class PartnerProductsController : BaseController
    {
        private readonly IPartnerProductService _partnerProductService;

        public PartnerProductsController(IPartnerProductService partnerProductService)
        {
            _partnerProductService = partnerProductService;
        }

        [HttpGet("transaction-generator")]
        public async Task<IActionResult> GenerateTranNo()
           => Ok(new Response
           {
               Code = 200,
               Message = "Success",
               Data = await Task.FromResult(_partnerProductService.GenerateTransactionNumber())
           });

        [HttpPost("hamkor-uchun-yuk")]
        public async Task<IActionResult> AddAsync(long productId, long partnerId, long userId, decimal quantityToMove, string transNo)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _partnerProductService.MoveProductToPartnerProductAsync(productId, partnerId, userId, quantityToMove, transNo)
            });

        [HttpPost("chegirma-berish/{id}/{discountPercentage}")]
        public async Task<IActionResult> CalculateAsync(long id, short discountPercentage)
           => Ok(new Response
           {
               Code = 200,
               Message = "Success",
               Data = await _partnerProductService.CalculeteDiscountPercentageAsync(id, discountPercentage)
           });

        [HttpPut("pay-for-product")]
        public async Task<IActionResult> UpdatePaidAsync(long partnerId, decimal paid)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _partnerProductService.PayForProductsAsync(partnerId, paid)
            });

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _partnerProductService.RetrieveByIdAsync(id)
            });

        [HttpGet]
        public async Task<IActionResult> GeAllAsync([FromQuery] PaginationParams @params)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _partnerProductService.RetrieveAllAsync(@params)
            });

        [HttpGet("get-by-transaction-number")]
        public async Task<IActionResult> GetByTransNo(string transNo)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _partnerProductService.RetrieveByTransNoAsync(transNo)
            });


        [HttpGet("yuk-tarixini-korish/{userId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetAllAsync(long userId, DateTime startDate, DateTime endDate)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _partnerProductService.RetrieveAllWithDateTimeAsync(userId, startDate, endDate)
            });

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyAsync(long id, [FromBody] PartnerProductForUpdateDto stockProductForUpdate)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _partnerProductService.ModifyAsync(id, stockProductForUpdate)
            });

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _partnerProductService.RemoveAsync(id)
            });
    }
}
