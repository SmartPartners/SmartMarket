using SmartMarket.Domin.Commons;
using SmartMarket.Domin.Entities.Cards;
using SmartMarket.Domin.Entities.Partners;
using SmartMarket.Domin.Entities.Products;
using SmartMarket.Domin.Enums;

namespace SmartMarket.Domin.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public bool IsActive { get; set; }
    public UserRole Role { get; set; }
    public decimal? Oylik { get; set; }
    public decimal? OlganPuli { get; set; }
    public decimal? QolganPuli { get; set; }

    public IEnumerable<Card> Cards { get; set; }
    public IEnumerable<Product> Products { get; set; }
    public IEnumerable<PartnerProduct> PartnerProducts { get; set; }
}
