using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.ProductDTO
{
    public class CreatedProductDTO
    {
        public int ProductTemplateId { get; set; }
        public Dictionary<int, List<int>> Ingredients { get; set; }

    }
}
