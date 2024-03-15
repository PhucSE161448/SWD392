using Restaurant.Application.ViewModels.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Help
{
    public interface IHelpService
    {
        Task SendEmail(Message message);
    }
}
