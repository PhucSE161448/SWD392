using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.Products;
using Restaurant.Application.Interfaces;
using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Application.ViewModels.OrderDTO;
using Restaurant.Application.Interfaces.Orders;

namespace Restaurant.WebAPI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IClaimsService _claimsService;
        public OrderController(IOrderService orderService, IClaimsService claimsService)
        {
            _orderService = orderService;
            _claimsService = claimsService;
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetOrderByUser()
        {
            var result = await _orderService.GetAllOrderByUser();
            return Ok(result);
        }
        [HttpGet("Detail")]
        public async Task<IActionResult> GetOrderDetail(int OrderId)
        {
            var result = await _orderService.GetOrderByUser(OrderId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderCreate)
        {
            var result = await _orderService.CreateOrder(orderCreate);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("Status/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromQuery] string status, int paymentId)
        {
            var result = await _orderService.UpdateOrderStatus(id, status, paymentId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }


}
