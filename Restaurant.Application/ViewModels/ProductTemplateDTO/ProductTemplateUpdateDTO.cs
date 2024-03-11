﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.ProductTemplateDTO
{
    public class ProductTemplateUpdateDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;
        public IFormFile? Image { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = null!;
        public string Description { get; set; }
        public int StoreId { get; set; }
    }
}
