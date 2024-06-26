﻿namespace SmartMarket.Service.DTOs.ContrAgents;

public record ContrAgentForUpdateDto
{
    public string Firma { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Dept { get; set; }
    public decimal PayForDept { get; set; }
    public decimal LastPaid { get; set; }
}
