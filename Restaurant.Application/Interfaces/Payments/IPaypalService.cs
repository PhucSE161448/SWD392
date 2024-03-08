using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Payments
{
    public interface IPaypalService
    {
        APIContext GetAPIContext(string clientId, string clientSecret, string mode);
        Dictionary<string, string> GetConfig(string _mode);
        string GetAccessToken(string _ClientId, string _ClientSecret, string _mode);
    }
}
