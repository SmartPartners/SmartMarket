namespace SmartMarket.Service.DTOs.Orders;

public record OrderForUpdateDto
{
    public decimal Quantity { get; set; }
    public long YiguvchId { get; set; }
    public long YukTaxlovchId { get; set; }
    public long PartnerId { get; set; }
}
