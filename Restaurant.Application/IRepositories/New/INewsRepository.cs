using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.IRepositories.New
{
    public interface INewsRepository : IGenericRepository<News>
    {
        public Task<List<News>> GetListNewBySize(string size = null);
    }
}
