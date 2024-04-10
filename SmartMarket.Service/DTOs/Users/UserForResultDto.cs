using SmartMarket.Domin.Entities.Cards;
using SmartMarket.Domin.Entities.Partners;
using SmartMarket.Domin.Entities.Products;
using SmartMarket.Domin.Enums;

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

    public ICollection<Card> Cards { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<PartnerProduct> PartnerProducts { get; set; }
}
