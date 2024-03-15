using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.Help;
using Restaurant.Application.Interfaces.Ingredient;
using Restaurant.Application.ViewModels.Help;

namespace Restaurant.WebAPI.Controllers.Help
{
    public class HelpController : BaseController
    {
        private readonly IHelpService _helpService;
        public HelpController(IHelpService helpService)
        {
            _helpService = helpService;
        }
        [HttpPut("SendHelp")]
        public void SendEmail([FromBody] Message message)
        {
            //var message = new Message(new string[] { "nhatht.02@gmail.com" }, "FEED", helpDetail);
            _helpService.SendEmail(message);
        }
    }
}
