using SmartMarket.Domin.Commons;
using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Domin.Entities.Users;
using SmartMarket.Domin.Enums;

namespace SmartMarket.Domin.Entities.Cards;

public class Card : Auditable
{
    public string TransNo { get; set; }
    public string ProductName { get; set; }
    public string BarCode { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public decimal CamePrice { get; set; }
    public decimal? SalePrice { get; set; }
    public decimal? PercentageOfPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public long? YukYiguvchId { get; set; }
    public User Yiguvchi { get; set; }
    public long CasherId { get; set; }
    public User Casher { get; set; }
    public string Status { get; set; }
    public OlchovBirligi OlchovBirligi { get; set; }
    public TolovUsuli TolovUsuli { get; set; }
}
