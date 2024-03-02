using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.New;
using Restaurant.Application.IRepositories.Products;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories.New
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        private readonly MixFoodContext _dbContext;

        public NewsRepository(
            MixFoodContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    
    }
}
