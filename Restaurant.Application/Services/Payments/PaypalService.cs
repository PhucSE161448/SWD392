using PayPal.Api;
using Restaurant.Application.Interfaces.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services.Payments
{
    public class PaypalService : IPaypalService
    {
        public string GetAccessToken(string _ClientId, string _ClientSecret, string _mode)
        {
            // getting accesstocken from paypal
            string accessToken = new OAuthTokenCredential(_ClientId, _ClientSecret, new Dictionary<string, string>()
            {
                {"mode",_mode}
            }).GetAccessToken();
            return accessToken;
        }
        public APIContext GetAPIContext(string _clientId, string _clientSecret, string _mode)
        {
            // return apicontext object by invoking it with the accesstoken  
            APIContext apiContext = new APIContext(GetAccessToken(_clientId, _clientSecret, _mode));
            apiContext.Config = GetConfig(_mode);
            return apiContext;
        }

        public Dictionary<string, string> GetConfig(string _mode)
        {
            return new Dictionary<string, string>()
            {
                {"mode",_mode}
            };
        }
    }
}
