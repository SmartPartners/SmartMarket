using Microsoft.AspNetCore.Mvc;
using SmartMarket.Api.Controllers.Commons;
using SmartMarket.Domin.Configurations;
using SmartMarket.Service.Interfaces.Cards;

namespace SmartMarket.Api.Controllers.Cards
{
    public class CardsController :  BaseController
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("transaction-generator")]
        public async Task<IActionResult> GenerateTranNo()
            => Ok(await Task.FromResult(_cardService.GenerateTransactionNumber()));

        [HttpPost("id-bilan-sotish")]
        public async Task<IActionResult> PostAsync(long id, long userId, decimal quantityToMove, string trnasNo)
           => Ok(await _cardService.MoveProductToCardAsync(id, userId, quantityToMove, trnasNo));

        [HttpPost("barcode-bilan-sotish")]
        public async Task<IActionResult> SaleAsync(string barCode, long userId, decimal quantityToMove, string trnasNo)
           => Ok(await _cardService.SaleProductWithBarCodeAsync(barCode, userId, quantityToMove, trnasNo));


        [HttpPost("chegirma-berish/{id}/{discountPercentage}")]
        public async Task<IActionResult> CalculateAsync(long id, short discountPercentage)
           => Ok(await _cardService.CalculeteDiscountPercentageAsync(id, discountPercentage));

        [HttpPost("{transNo}")]
        public async Task<IActionResult> Updateasync([FromRoute(Name = "transNo")] string transNo)
            => Ok(await _cardService.UpdateWithTransactionNumberAsync(transNo));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _cardService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
            => Ok(await _cardService.RetrieveByIdAsync(id));

        [HttpGet("barcode-orqali-olish/{barCode}")]
        public async Task<IActionResult> GetByBarCodeAsync([FromRoute(Name = "barCode")] string barCode)
            => Ok(await _cardService.GetByBarCodeAsync(barCode));

        [HttpGet("status-orqali-qidiruv/{status}")]
        public async Task<IActionResult> GetBystatusAsync([FromRoute(Name = "status")] string status)
            => Ok(await _cardService.SvetUchgandaAsync(status));

        [HttpGet("tarixni-korish/{userId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetAllCardProductsAsync(long userId, DateTime startDate, DateTime endDate)
            => Ok(await _cardService.RetrieveAllWithDateTimeAsync(userId, startDate, endDate));

        [HttpGet("kop-sotilgan-yuk/{max}")]
        public async Task<IActionResult> GetByMaxAsync([FromRoute(Name = "max")] int max)
            => Ok(await _cardService.RetrieveAllWithMaxSaledAsync(max));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
            => Ok(await _cardService.ReamoveAsync(id));

        [HttpPost("planshetdan-donalab-yukni-qaytarish")]
        public async Task<IActionResult> YukniQaytarish(string transNo, string barCode, decimal quantity)
            =>Ok(await _cardService.CancelProductAtListByPlanshetAsync(transNo, barCode, quantity));

       /* [HttpPost("planshetdan-donalab-yukni-qaytarish")]
        public async Task<IActionResult> ListQilibYukniQaytarish([FromBody] List<(string transNo, string barCode, decimal quantity)> productList)
            => Ok(await _cardService.CancelProductsAtListByPlanshetAsync(productList));*/
    }
}
