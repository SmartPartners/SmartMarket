using SmartMarket.Domin.Commons;
using SmartMarket.Domin.Entities.Cards;
using SmartMarket.Domin.Entities.Partners;
using SmartMarket.Domin.Entities.Products;

namespace SmartMarket.Domin.Entities.Categories;

public class Category : Auditable
{
    public string Name { get; set; }

    public IEnumerable<Card> Cards { get; set; }
    public IEnumerable<Product> Products { get; set; }
    public IEnumerable<PartnerProduct> PartnersProducts { get; set; }
}
