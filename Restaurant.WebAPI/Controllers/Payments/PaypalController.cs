using Application.ViewModels.AccountDTO;
using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Accounts;
using Restaurant.Application.Interfaces.Orders;
using Restaurant.Application.Interfaces.Payments;
using Restaurant.Application.Interfaces.Products;
using Restaurant.Application.IRepositories.Accounts;
using Restaurant.Application.ViewModels.OrderDTO;

namespace Restaurant.WebAPI.Controllers.Payments
{
    public class PaypalController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaypalService _paypalService;
        private readonly IProductService _productService;
        private readonly IAccountService _accountService;
        private readonly IOrderService _orderService;
        private readonly IClaimsService _claimsService;
        private readonly ILogger<PaypalController> _logger;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        IConfiguration _configuration;
        private PayPal.Api.Payment payment;
        public PaypalController(IPaypalService paypalService, ILogger<PaypalController> logger, IHttpContextAccessor context, IConfiguration iconfiguration,
            IProductService productService, IAccountService accountService, IOrderService orderService, IMapper mapper, IUnitOfWork unitOfWork
            , IClaimsService claimsService)
        {
            _logger = logger;
            _httpContextAccessor = context;
            _configuration = iconfiguration;
            _paypalService = paypalService;
            _productService = productService;
            _accountService = accountService;
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
            _claimsService = claimsService;
        }

        [HttpGet("PaymentPaypal/{accountId}")]
        public async Task<ActionResult<string>> PaymentWithPaypal([FromRoute] int accountId, string paymentId = "", string Cancel = null, string blogId = "", string PayerID = "")
        {
            //getting the apiContext  
            var ClientID = _configuration.GetValue<string>("PayPal:Key");
            var ClientSecret = _configuration.GetValue<string>("PayPal:Secret");
            var mode = _configuration.GetValue<string>("PayPal:mode");

            APIContext apiContext = _paypalService.GetAPIContext(ClientID, ClientSecret, mode);

            // apiContext.AccessToken="Bearer access_token$production$j27yms5fthzx9vzm$c123e8e154c510d70ad20e396dd28287";
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = PayerID;
                if (string.IsNullOrEmpty(payerId))
                {

                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/api/Paypal/CreatedPayment/";
                    //return baseURI;
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    //var guidd = Convert.ToString((new Random()).Next(100000));
                    Guid guidd = Guid.NewGuid();
                    int AccountId = accountId;
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = await this.CreatePayment(apiContext, baseURI + guidd, AccountId, blogId);

                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    //return paypalRedirectUrl;
                    // saving the paymentID in the key guid  
                    //_httpContextAccessor.HttpContext.Session.SetString("payment", createdPayment.id);
                    // Thiết lập giá trị của Cookie
                    //Response.Cookies.Append("payment", createdPayment.id);
                    //return Redirect(paypalRedirectUrl);
                    return Ok(new
                    {
                        message = "redirect",
                        url = paypalRedirectUrl,
                        paymentId = createdPayment.id
                    });
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  

                    //var paymentId = _httpContextAccessor.HttpContext.Session.GetString("payment");
                    // Truy xuất giá trị của Cookie
                    var executedPayment = ExecutePayment(apiContext, payerId, paymentId);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return Redirect($"https://ctqmmec.azurewebsites.net/paymentfail/{accountId}");
                    }
                    var blogIds = executedPayment.transactions[0].item_list.items[0].sku;
                    CustomerPaymentDTO customerData = new CustomerPaymentDTO
                    {
                        AccountId = accountId,
                        Status = "Paypal"
                    };
                    //await _orderService.CustomerPayment(customerData);
                    return Redirect($"https://ctqmmec.azurewebsites.net/paymentsuccess/{accountId}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private async Task<Payment> CreatePayment(APIContext apiContext, string redirectUrl, int accountId, string blogId)
        {
            //create itemlist and add item objects to it  


            //----------------------------SỬA Ở ĐÂY-----------------------------------
            var userId = _claimsService.GetCurrentUserId;
            var data = await _productService.GetAllProductAsync(userId);
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  


            //----------------------------SỬA Ở ĐÂY-----------------------------------
            //itemList.items.Add(new Item()
            //{
            //    name = userId,
            //    currency = "USD",
            //    price = data...ToString(),
            //    //quantity = data.totalAmount.ToString(),
            //    sku = "asd"
            //});
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            /*var details = new Details()
            {
                tax = "1",
                shipping = "140",
                subtotal = "1"
            };*/
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",


                //----------------------------SỬA Ở ĐÂY-----------------------------------
                //total = data.totalDiscount.ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                //details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Guid.NewGuid().ToString(), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
    }
}
