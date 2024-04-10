using SmartMarket.Domin.Enums;
using SmartMarket.Service.DTOs.CancelOrders;
using SmartMarket.Service.DTOs.Cards;
using SmartMarket.Service.DTOs.PartnerProducts;
using SmartMarket.Service.DTOs.Products;

namespace SmartMarket.Service.DTOs.Users;

public record UserForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public UserRole Role { get; set; }
    public decimal? Oylik { get; set; }
    public decimal? OlganPuli { get; set; }
    public decimal? QolganPuli { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }


    public ICollection<CardForResultDto> Cards { get; set; }
    public ICollection<ProductForResultDto> Products { get; set; }
    public ICollection<CancelOrderForResultDto> CancelOrders { get; set; }
    public ICollection<PartnerProductForResultDto> PartnerProducts { get; set; }
}
