﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.ViewModels.CategoryDTO
{
    public class CategoryDto
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
