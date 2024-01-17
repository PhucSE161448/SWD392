using Restaurant.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories
{
    public class GenericRepository <T> : IGenericRepository<T> where T : class
    {
    }
}
