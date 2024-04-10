using SmartMarket.Service.DTOs.PartnerProducts;
using SmartMarket.Service.DTOs.Products;

namespace SmartMarket.Service.DTOs.Categories;

public record CategoryForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<ProductForResultDto> Products { get; set; }
    public ICollection<PartnerProductForResultDto> PartnerProducts { get; set; }
}