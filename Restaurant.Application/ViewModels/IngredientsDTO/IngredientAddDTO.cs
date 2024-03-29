﻿
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.IngredientsDTO
{
    public class IngredientAddDTO
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Calo { get; set; }
        public string Description { get; set; } = null!;
        public IFormFile? Image {  get; set; } 
        public int IngredientTypeId { get; set; }
    }
}
