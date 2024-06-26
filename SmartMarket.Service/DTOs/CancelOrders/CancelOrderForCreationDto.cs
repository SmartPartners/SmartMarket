﻿using SmartMarket.Domin.Enums;

namespace SmartMarket.Service.DTOs.CancelOrders;

public record CancelOrderForCreationDto
{
    public string TransNo { get; set; }
    public string ProductName { get; set; }
    public string BarCode { get; set; }
    public long CategoryId { get; set; }
    public decimal Price { get; set; }
    public OlchovBirligi OlchovTuri { get; set; }
    public decimal Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public long CasherId { get; set; }
    public string Rason { get; set; }
    public long CancelerCasherId { get; set; }
    public DateTime ReturnDate { get; set; }
    public bool Action { get; set; }
    public string Status { get; set; }
}
