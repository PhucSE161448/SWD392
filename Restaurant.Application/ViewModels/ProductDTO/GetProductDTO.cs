using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.ProductDTO
{
    public class GetProductDTO
    {
        public List<ProductsDTO> ProductDTOs { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
