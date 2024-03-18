using Restaurant.Application.ViewModels.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.OrderDTO
{
    public class OrderDetailDTO
    {
        public int OrderId {  get; set; }
        public List<ProductsDTO> details { get; set; }
    }
}
