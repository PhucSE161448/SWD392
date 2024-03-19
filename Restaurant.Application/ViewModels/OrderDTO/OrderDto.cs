using Application.ViewModels.AccountDTO;
using Restaurant.Application.ViewModels.PaymentDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.OrderDTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public double TotalPrice { get; set; }
        public int StoreId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public AccountsDTO? Accounts { get; set; }
        public PaymentsDTO? PaymentMethods { get; set; }
    }
}
