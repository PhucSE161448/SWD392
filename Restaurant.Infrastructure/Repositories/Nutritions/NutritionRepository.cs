using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.Nutritions;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories.Nutritions
{
    public class NutritionRepository : GenericRepository<Nutrition>, INutritionRepository
    {
        private readonly MixFoodContext _dbContext;
        public NutritionRepository(MixFoodContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}
