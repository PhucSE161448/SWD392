using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> CheckProductExited(int id);
        Task<IEnumerable<Product>> SearchProductByNameAsync(string name);
        Task<IEnumerable<Product>> GetSortedProductAsync();
    }
}
