using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.TemplateSteps;
using Restaurant.Application.ViewModels.TemplateStepsDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories.TemplateSteps
{
    public class TemplateStepRepository : GenericRepository<TemplateStep>, ITemplateStepRepository
    {
        private readonly MixFoodContext _dbContext;
        private readonly IMapper _mapper;
        public TemplateStepRepository(
            MixFoodContext context,
            ICurrentTime timeService,
            IClaimsService claimsService,
            IMapper mapper
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateTemplateAsync(TemplateStepCreateDTO templateStep)
        {
            try
            {
                var template = _mapper.Map<TemplateStep>(templateStep);
                await _dbContext.TemplateSteps.AddAsync(template);
                await _dbContext.SaveChangesAsync();
                foreach (var ingredientId in templateStep.IngredientTypeId)
                {
                    var ingredientProduct = new IngredientTypeTemplateStep
                    {
                        TemplateStepId = template.Id,
                        IngredientTypeId = ingredientId,
                        QuantityMax = templateStep.Max,
                        QuantityMin = templateStep.Min,
                    };
                    await _dbContext.IngredientTypeTemplateSteps.AddAsync(ingredientProduct);
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }
    }
}
