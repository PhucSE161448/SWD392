using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using Restaurant.Application.Interfaces.Orders;

namespace Restaurant.WebAPI.Controllers.Payments
{
    public class PayOSController : BaseController
    {
        private readonly PayOS _payOS;
        private readonly IOrderService _orderService;
        public PayOSController(PayOS payOS, IOrderService orderService)
        {
            _payOS = payOS;
            _orderService = orderService;
        }

        [HttpPost("PayOS/{orderId}")]
        public async Task<IActionResult> Checkout([FromRoute] int orderId)
        {
            try
            {
                var itemss = await _orderService.GetOrderById(orderId);
                var oder = await _orderService.GetOrderByUser(orderId);
                List<ItemData> items = new List<ItemData>();
                foreach (var detail in oder.Data.details)
                {
                    ItemData item = new ItemData(detail.Name, 1, (int)itemss.Data.TotalPrice);
                    items.Add(item);
                }

                PaymentData paymentData = new PaymentData(itemss.Data.Id, (int)itemss.Data.TotalPrice, "Thanh toan don hang", items, "https://mixed-food.vercel.app/payment/cancel", "https://mixed-food.vercel.app/payment/success");
                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);
                return Ok(new
                {
                    message = "redirect",
                    url = createPayment.checkoutUrl
                });
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return Redirect("https://mixed-food.vercel.app");
            }
        }
    }
}
