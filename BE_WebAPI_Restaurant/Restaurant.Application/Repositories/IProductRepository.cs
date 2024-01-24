using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreatedProduct(int id);
    }
}
