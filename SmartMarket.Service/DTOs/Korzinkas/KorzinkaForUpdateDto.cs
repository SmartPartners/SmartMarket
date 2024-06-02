namespace SmartMarket.Service.DTOs.Korzinkas;

public record KorzinkaForUpdateDto
{
    public decimal DiscountPrice { get; set; }
    public decimal Quantity { get; set; }
}
