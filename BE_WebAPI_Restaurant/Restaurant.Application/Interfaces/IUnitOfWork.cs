using Restaurant.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; }
        public IProductRepository ProductRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
