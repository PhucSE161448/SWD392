using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.TemplateSteps;
using Restaurant.Application.ViewModels.Ingredient_TypeDTO;
using Restaurant.Application.ViewModels.IngredientsDTO;
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

        public async Task<(bool success, TemplateStep templateStep)> CreateTemplateAsync(TemplateStepCreateDTO templateStep)
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
                return (true, template);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public async Task<IEnumerable<TemplateStepIngredientDTO>> GetTemplateStepsByProductId(int productTemplateId)
        {
            var result = await (from ts in _dbContext.TemplateSteps
                                where ts.ProuctTemplateId == productTemplateId
                                select new TemplateStepIngredientDTO
                                {
                                    TemplateStep = _mapper.Map<TemplateStepDTO>(ts),
                                    Ingredients = (from its in _dbContext.IngredientTypeTemplateSteps
                                                   join i in _dbContext.Ingredients
                                                   on its.IngredientTypeId equals i.IngredientTypeId
                                                   where its.TemplateStepId == ts.Id
                                                   select new IngredientDTO
                                                   {
                                                       Id = i.Id,
                                                       Name = i.Name,
                                                       ImageUrl = i.ImageUrl,
                                                       Calo = i.Calo,
                                                       Price = i.Price,
                                                      // IngredientType = _mapper.Map<IngredientTypeDTO>(i.IngredientType)
                                                   })
                                                   .ToList(),
                                    IngredientTypes = (from its in _dbContext.IngredientTypeTemplateSteps
                                                       join it in _dbContext.IngredientTypes
                                                       on its.IngredientTypeId equals it.Id
                                                       where its.TemplateStepId == ts.Id
                                                       select new IngredientTypeDTO
                                                       {
                                                           Id = it.Id,
                                                           Name = it.Name,
                                                       }).ToList()

                                }).ToListAsync();
            return result;
        }

    }
}

