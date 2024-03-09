using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.ProductDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public decimal Price { get; set; }
        public int ProductTemplateId { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
