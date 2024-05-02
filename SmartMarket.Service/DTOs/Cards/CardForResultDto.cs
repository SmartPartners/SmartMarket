using SmartMarket.Domin.Enums;

namespace SmartMarket.Service.DTOs.Cards;

public record CardForResultDto
{
    public long Id { get; set; }
    public string TransNo { get; set; }
    public string ProductName { get; set; }
    public string BarCode { get; set; }
    public long CategoryId { get; set; }
    public decimal CamePrice { get; set; }
    public decimal? SalePrice { get; set; }
    public decimal? PercentageOfPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public long CasherId { get; set; }
    public string Status { get; set; }
    public OlchovBirligi OlchovBirligi { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
