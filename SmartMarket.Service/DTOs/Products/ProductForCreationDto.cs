﻿using Microsoft.AspNetCore.Http;
using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Domin.Entities.Users;
using SmartMarket.Domin.Enums;

namespace SmartMarket.Service.DTOs.Products;

public record ProductForCreationDto
{
    public string BarCode { get; set; }
    public string Name { get; set; }
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public decimal CamePrice { get; set; }
    public decimal Quantity { get; set; }
    public OlchovBirligi OlchovTuri { get; set; }
    public decimal? SalePrice { get; set; }
    public decimal? PercentageOfPrice { get; set; }
    public bool Action {  get; set; }
    public string ImagePath { get; set; }
    public long ContrAgentId { get; set; }
}
