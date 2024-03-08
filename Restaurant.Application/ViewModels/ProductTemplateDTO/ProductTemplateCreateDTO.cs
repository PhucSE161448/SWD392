using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.ProductTemplateDTO
{
    public class ProductTemplateCreateDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public int? Quantity { get; set; }
        public string? Size { get; set; } 
        public IFormFile? Image { get; set; }
        public decimal Price { get; set; }
        public int StoreId { get; set; }
    }
}
