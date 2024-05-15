using SmartMarket.Domin.Enums;
using SmartMarket.Service.DTOs.PartnerProducts;

namespace SmartMarket.Service.DTOs.Partners;

public record PartnerForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Debt { get; set; }
    public decimal Paid { get; set; }
    public TolovUsuli TolovUsuli { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<PartnerProductForResultDto> PartnerProducts { get; set; }
}
