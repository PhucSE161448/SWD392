using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;


namespace Restaurant.Application.Utils
{
    public static class SendEmail
    {
        public static async Task<bool> SendConfirmationEmail(string toEmail, string confirmationLink)
        {
            // Thông tin tài khoản email gửi
            var userName = "MIXFOOD";
            //Tài khoản GMAIL thật
            var emailFrom = "nhatht.02@gmail.com";
            //Tạo mật khẩu ứng dụng cho GMAIL rồi paste vào đây
            var password = "gpba qhps jjun hdwo";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(userName, emailFrom));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Confirmation Email";

            // message.Body = new TextPart("plain")
            // {
            //     Text = $"Please click the link below to confirm your email:\n{confirmationLink}"
            // };
            message.Body = new TextPart("html")
            {
                Text =
        @"
        <html>
            <head>
                <style>
                    body {
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        height: 100vh;
                        margin: 0;
                        font-family: 'Arial', sans-serif;
                        background-color: #fff3e0; /* Màu nền nhẹ nhàng */
                    }
                    .content {
                        text-align: center;
                        padding: 20px;
                        background-color: #ffe0b2; /* Màu nền cho khối nội dung */
                        border-radius: 10px;
                        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* Thêm bóng cho khối nội dung */
                    }
                    .header {
                        color: #ff6f00; /* Màu cho tiêu đề */
                        font-size: 24px;
                        margin-bottom: 20px;
                    }
                    .button {
                        display: inline-block;
                        padding: 10px 20px;
                        background-color: #ff9800; /* Màu nền cho nút */
                        color: #ffffff; /* Màu chữ cho nút */
                        text-decoration: none;
                        border-radius: 5px;
                        font-size: 16px;
                        transition: background-color 0.3s ease; /* Hiệu ứng khi hover */
                    }
                    .button:hover {
                        background-color: #fb8c00; /* Thay đổi màu khi hover */
                    }
                </style>
            </head>
            <body>
                <div class='content'>
                    <div class='header'>Welcome to MixFoodie's Heaven!</div>
                    <p>Please click the button below to confirm your email and start exploring delicious recipes:</p>
                    <a class='button' href='"
        + confirmationLink
        + @"'>Confirm Email</a>
                </div>
            </body>
        </html>
    "
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // Thiết lập xác thực với tài khoản Gmail
                client.Authenticate(emailFrom, password);

                try
                {
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
