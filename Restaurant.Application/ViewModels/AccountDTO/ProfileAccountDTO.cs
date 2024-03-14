using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.AccountDTO
{
    public class ProfileAccountDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
    }
}
