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
        public List<int> IngredientId {  get; set; }

        public int Quantity { get; set; }
    }
}
