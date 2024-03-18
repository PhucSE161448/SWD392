using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.OrderDTO;
using Restaurant.Application.ViewModels.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Orders
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderDto>> CreateOrder(OrderCreateDTO orderCreate);
        Task<ServiceResponse<OrderDto>> UpdateOrderStatus(int id, string status);
        Task<ServiceResponse<OrderDto>> GetOrderById(int id);
        Task<ServiceResponse<OrderDetailDTO>> GetOrderByUser(int OrderId);
        Task<ServiceResponse<List<OrderDto>>> GetAllOrderByUser();

    }
}
