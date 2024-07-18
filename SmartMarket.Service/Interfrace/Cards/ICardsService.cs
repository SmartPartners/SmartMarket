using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Enums;
using SmartMarket.Service.DTOs.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Interfrace.Cards
{
    public interface ICardsService
    {
        Task<string> GenerateTransactionNumber();
        Task<CardForResultDto> SaleProductWithId(long id, long userId, decimal quantity, string transNo);
        Task<CardForResultDto> SaleProductWithBarCodeAsync(string barcode, long userId, decimal quantity, string transNo);
        Task<CardForResultDto> CalculeteDiscountPercentageAsync(long id, short discount);
        Task<CardForResultDto> UpdateWithTransactionNumberAsync(string transNo, TolovUsuli tolovUsuli);
        Task<IEnumerable<CardForResultDto>> GetAllAsync(PaginationParams paginationParams);
        Task<CardForResultDto> GetByIdAsync(long id);
        Task<CardForResultDto> GetByBarCodeAsync(string barcode);
        Task<IEnumerable<CardForResultDto>> GetBystatusAsync(string barcode);
        Task<IEnumerable<CardForResultDto>> GetAllHistory(long userId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<CardForResultDto>> GetAllMaxSells(int max);
        Task<bool> DeleteAsync(long id);
        Task<bool> CancelProductAtListByPlanshetAsync(string transNo, string barCode, decimal quantity);
    }
}
