using Restaurant.Application.ViewModels.CategoryDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.ProductTemplateDTO
{
    public class ProductTemplateDTO
    {
        public int Id { get; set; }
       // public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int StoreId { get; set; }
        public string Description { get; set; }
        public CategoryDto Category {  get; set; }
    }
}
