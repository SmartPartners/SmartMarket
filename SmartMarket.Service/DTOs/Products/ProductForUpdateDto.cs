using SmartMarket.Domin.Enums;

namespace SmartMarket.Service.DTOs.Products;

public record ProductForUpdateDto
{
    public string PCode { get; set; }
    public string BarCode { get; set; }
    public string Name { get; set; }
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public decimal CamePrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public OlchovBirligi OlchovTuri { get; set; }
    public decimal? SalePrice { get; set; }
    public decimal? PercentageOfPrice { get; set; }
}
