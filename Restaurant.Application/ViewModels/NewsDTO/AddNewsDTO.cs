using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.NewsDTO
{
    public class AddNewsDTO
    {

        public int AccountId { get; set; }
        public string Description { get; set; } = null!;
        //public string Image { get; set; } = null!;
        public IFormFile? formFile { get; set; }
        public string Status { get; set; } = null!;
        public string Title { get; set; } = null;
    }
}
