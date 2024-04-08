using SmartMarket.Domin.Commons;
using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Domin.Entities.Users;

namespace SmartMarket.Domin.Entities.Partners;

public class PartnerProduct : Auditable
{
    public long PartnerId { get; set; }
    public Partner Partner { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public string ProductName { get; set; }
    public string TransNo { get; set; }
    public string PCode { get; set; }
    public string BarCode { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}
