using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Commons
{
    public class JWTSection
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
    public class EmailConfig
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class PayOSConfig
    {
        public string PAYOS_CHECKSUM_KEY { get; set; }
        public string PAYOS_API_KEY { get; set; }
        public string PAYOS_CLIENT_ID { get; set; }
    }
    public class AppConfiguration
    {
        public string DatabaseConnection { get; set; }
        public JWTSection JWTSection { get; set; }
        public EmailConfig EmailConfiguration { get; set; }  
        public PayOSConfig PayOSConfig { get; set; }
    }
}
