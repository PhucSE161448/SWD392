using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.IRepositories.Categories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> CheckCategoryExited(string name);
    }
}
