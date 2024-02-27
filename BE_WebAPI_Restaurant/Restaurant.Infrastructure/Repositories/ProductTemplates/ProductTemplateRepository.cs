using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.ProductTemplates;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories.ProductTemplates
{
    public class ProductTemplateRepository : GenericRepository<ProductTemplate>, IProductTemplateRepository
    {
        private readonly MixFoodContext _dbContext;
        public ProductTemplateRepository(MixFoodContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}
