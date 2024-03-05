using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.Ingredients;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories.Ingredients
{
    public class IngredientRepository : GenericRepository<Ingredient>,IIngredientRepository
    {
        private readonly MixFoodContext _dbContext;
        public IngredientRepository(MixFoodContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}
