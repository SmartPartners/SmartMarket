namespace SmartMarket.Service.DTOs.Cards;

public record CardForUpdateDto
{
    public string TransNo { get; set; }
    public string ProductName { get; set; }
    public string BarCode { get; set; }
    public long CategoryId { get; set; }
    public decimal Price { get; set; }
    public short DiscountPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public long CasherId { get; set; }
    public string Status { get; set; }
}
