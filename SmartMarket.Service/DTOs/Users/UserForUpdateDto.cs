﻿using SmartMarket.Domin.Enums;

namespace SmartMarket.Service.DTOs.Users;

public record UserForUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public UserRole Role { get; set; }
    public decimal? Oylik { get; set; }
    public decimal? OlganPuli { get; set; }
    public decimal? QolganPuli { get; set; }
}
