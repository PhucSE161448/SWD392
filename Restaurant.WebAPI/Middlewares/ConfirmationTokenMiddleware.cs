using Application;
using Restaurant.Application.Interfaces;

namespace Restaurant.WebAPI.Middlewares
{
    public class ConfirmationTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public ConfirmationTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Tạo một phạm vi dịch vụ tạm thời
            using (var scope = context.RequestServices.CreateScope())
            {
                // Lấy IUnitOfWork từ phạm vi dịch vụ tạm thời
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var token = context.Request.Query["token"];

                if (!string.IsNullOrEmpty(token))
                {
                    var user = await unitOfWork.AccountRepository.GetUserByConfirmationToken(token);
                    if (user != null && user.IsConfirmed != true)
                    {
                        // Xác nhận tài khoản
                        user.IsConfirmed = true;
                        //user.ConfirmationToken = null;
                        await unitOfWork.SaveChangeAsync();

                        var emailContent = $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: 'Arial', sans-serif; background-color: #fffbeb; }}
                        .container {{ padding: 20px; background-color: #fff; border-radius: 10px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); }}
                        .header {{ font-size: 24px; font-weight: bold; color: #ff8c00; background-color: #ffe4b3; padding: 15px; border-radius: 10px 10px 0 0; text-align: center; }}
                        .message {{ margin-top: 20px; color: #555; line-height: 1.5; }}
                        .footer {{ font-size: 12px; text-align: center; margin-top: 20px; color: #999; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>Email Confirmation</div>
                        <div class='message'>
                            <p>Your email has been confirmed successfully!</p>
                            <p>Thank you for verifying your email address. You can now access all the features of our service.</p>
                        </div>
                        <div class='footer'>
                            <p>&copy; 2024 Your Company. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync(emailContent);
                        return;
                    }
                }
            }
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("<html><body><p>Invalid token or account already confirmed.</p></body></html>");
            await _next(context);
        }
    }
}