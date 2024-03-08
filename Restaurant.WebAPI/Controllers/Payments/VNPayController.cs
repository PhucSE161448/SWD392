using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.Accounts;
using Restaurant.Application.Interfaces.Orders;
using Restaurant.Application.Interfaces.Payments;
using Restaurant.Application.Interfaces.Products;
using Restaurant.Application.ViewModels.OrderDTO;
using Restaurant.Application.ViewModels.PayLib;

namespace Restaurant.WebAPI.Controllers.Payments
{
    public class VNPayController : BaseController
    {
        private readonly IVNPayService _vnpayService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        private readonly ILogger<PaypalController> _logger;
        private IHttpContextAccessor _httpContextAccessor;
        //private readonly HttpContext _httpContext;
        IConfiguration _configuration;

        public VNPayController(IVNPayService vnpayService, ILogger<PaypalController> logger, IHttpContextAccessor context, IConfiguration iconfiguration,
                    IProductService productService, IAccountService accountService, IOrderService orderService)
        {
            _logger = logger;
            _httpContextAccessor = context;
            _configuration = iconfiguration;
            _vnpayService = vnpayService;
            _productService = productService;
            _accountService = accountService;
            _orderService = orderService;
            //_httpContext = httpContext;
        }

        [Authorize]
        [HttpGet("PaymentVNPay/{accountId}")]
        public async Task<ActionResult<string>> Payment([FromRoute] int accountId)
        {
            var url = _configuration.GetValue<string>("VNPay:Url");
            var returnUrl = _configuration.GetValue<string>("VNPay:ReturnUrl");
            var tmnCode = _configuration.GetValue<string>("VNPay:TmnCode");
            var hashSecret = _configuration.GetValue<string>("VNPay:HashSecret");

            PayLib pay = new PayLib();
            //----------------------------SỬA Ở ĐÂY-----------------------------------
            //var cartData = await _cartService.GetCustomerCart(customerId);
            Guid newGuid = Guid.NewGuid();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            //----------------------------SỬA Ở ĐÂY-----------------------------------
            //pay.AddRequestData("vnp_Amount", (cartData.totalDiscount * 23730).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", "123.21.100.96") /*HttpContext.Request.ToString())*/; //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "billpayment"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", newGuid.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            CustomerPaymentDTO customerPaymentDTO = new CustomerPaymentDTO
            {
                AccountId = accountId,
                Status = "VNPay"
            };
            //----------------------------SỬA Ở ĐÂY-----------------------------------
            //await _orderService.CustomerPayment(customerPaymentDTO);
            return Ok(new
            {
                message = "redirect",
                url = paymentUrl
            });
        }

        /*public string PaymentConfirm()
        {
            if (Request.QueryString.Count > 0)
            {
                //string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var hashSecret = _configuration.GetValue<string>("VNPay:HashSecret");
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        return "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        return "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    return "Có lỗi xảy ra trong quá trình xử lý";
                }
            }
            return "loi";
            //return View();
        }*/
    }
}
