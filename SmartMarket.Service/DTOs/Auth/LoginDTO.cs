﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.DTOs.Auth
{
    public class LoginDTO
    {
        public string PhoneNumber {  get; set; }
        public string Password {  get; set; }
    }
}
