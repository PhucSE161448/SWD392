using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.OrderDTO
{
    public class CustomerPaymentDTO
    {
        public int AccountId{ get; set; }
        public string Status { get; set; }
    }
}
