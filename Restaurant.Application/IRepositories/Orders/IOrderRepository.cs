using Restaurant.Application.ViewModels.OrderDTO;
using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.IRepositories.Orders
{
    public interface IOrderRepository
    {
        Task<(bool success, Order order)> CreateOrderAsync(OrderCreateDTO o);
        Task<Order> GetOrderById(int id);
        Task<OrderDetailDTO> GetOrderDetail(int id);
        Task<List<Order>> GetAllOrderByUser();
        Task<(bool success, Order order)> UpdateOrderStatusAsync(int id, string status);
    }
}
