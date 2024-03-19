using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.PaymentDTO
{
    public class PaymentsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PaymentType {  get; set; }
        public bool? IsDelete { get; set; }
    }
}
