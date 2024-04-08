using SmartMarket.Domin.Commons;

namespace SmartMarket.Domin.Entities.Partners;

public class Partner : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Debt { get; set; }
    public decimal Paid { get; set; }

    public IEnumerable<PartnerProduct> PartnersProducts { get; set; }
}
