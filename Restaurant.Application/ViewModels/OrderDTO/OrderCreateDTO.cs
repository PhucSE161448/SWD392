using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.OrderDTO
{
    public class OrderCreateDTO
    {
        public double TotalPrice { get; set; }
        public List<int> ProductId { get; set; }
    }
}
