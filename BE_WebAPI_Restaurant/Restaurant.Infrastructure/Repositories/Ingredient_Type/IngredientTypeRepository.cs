using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.Ingredient_Type;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories.Ingredient_Type
{
    public class IngredientTypeRepository : GenericRepository<IngredientType>, IIngredientTypeRepository
    {
        private readonly MixFoodContext _dbContext;
        public IngredientTypeRepository(MixFoodContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}
