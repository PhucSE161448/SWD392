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
    public class IngredientGroupDTO
    {
        public IngredientTypeDTO IngredientType { get; set; }
        public List<IngredientDTO> Ingredients { get; set; }
    }
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
                var productTemplate = await _dbContext.ProductTemplates.FirstOrDefaultAsync(x => x.Id == templateStep.ProductTemplateId);
                template.ProuctTemplateId = productTemplate.Id;
                template.Name = productTemplate.Name;
                template.CreatedDate = DateTime.Now;
                await _dbContext.TemplateSteps.AddAsync(template);
                await _dbContext.SaveChangesAsync();
                foreach (var ingredients in templateStep.IngredientType)
                {
                    var ingredientProduct = new IngredientTypeTemplateStep
                    {
                        TemplateStepId = template.Id,
                        IngredientTypeId = ingredients.IngredientTypeId,
                        QuantityMax = ingredients.QuantityMax,
                        QuantityMin = ingredients.QuantityMin,
                    };
                    await _dbContext.IngredientTypeTemplateSteps.AddAsync(ingredientProduct);
                }
                await _dbContext.SaveChangesAsync();
                var tem = await _dbContext.TemplateSteps.Include(x => x.IngredientTypeTemplateSteps).FirstOrDefaultAsync(x => x.Id == template.Id);
                return (true, tem);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }
        public async Task<(bool success, TemplateStep templateStep)> UpdateTemplateAsync(int id, TemplateStepUpdateDTO templateStep)
        {
            try
            {
                var template = await _dbContext.TemplateSteps.FindAsync(id);
                template.ModifiedDate = DateTime.Now;
                var existingIngredientTypeTemplateSteps = _dbContext.IngredientTypeTemplateSteps
                    .Where(its => its.TemplateStepId == template.Id)
                    .ToList();
                if (existingIngredientTypeTemplateSteps.Any())
                {
                    _dbContext.IngredientTypeTemplateSteps.RemoveRange(existingIngredientTypeTemplateSteps);
                }
               

               
                foreach (var ingredientId in templateStep.IngredientTypeTemplateSteps)
                {
                    var ingredientProduct = new IngredientTypeTemplateStep
                    {
                        TemplateStepId = template.Id,
                        IngredientTypeId = ingredientId.IngredientTypeId,
                        QuantityMax = ingredientId.QuantityMax,
                        QuantityMin = ingredientId.QuantityMin,
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
        public async Task<bool> DeleteTemplateAsync(int TemplateStepId)
        {
            var relatedIngredientTypeTemplateSteps = _dbContext.IngredientTypeTemplateSteps
                                                     .Where(its => its.TemplateStepId == TemplateStepId)
                                                     .ToList();
            try
            {
                _dbContext.IngredientTypeTemplateSteps.RemoveRange(relatedIngredientTypeTemplateSteps);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
            public async Task<IEnumerable<TemplateStepIngredientDTO>> GetTemplateStepsByProductId(int? productTemplateId)
            {
                IQueryable<TemplateStep> templateStepsQuery = _dbContext.TemplateSteps;

                if (productTemplateId.HasValue)
                {
                    templateStepsQuery = templateStepsQuery.Where(ts => ts.ProuctTemplateId == productTemplateId);
                }

                var result = await (from ts in templateStepsQuery.Include(x => x.IngredientTypeTemplateSteps)
                                    select new
                                    {
                                        TemplateStep = _mapper.Map<TemplateStepDTO>(ts),
                                        Ingredients = _dbContext.IngredientTypeTemplateSteps
                                                    .Where(its => its.TemplateStepId == ts.Id)
                                                    .Join(_dbContext.Ingredients.Where(i => i.IsDeleted == false),
                                                          its => its.IngredientTypeId,
                                                          i => i.IngredientTypeId,
                                                          (its, i) => new { its, i })
                                                    .Join(_dbContext.IngredientTypes,
                                                          x => x.i.IngredientTypeId,
                                                          it => it.Id,
                                                          (x, it) => new { x.its, x.i, it })
                                                    .Select(x => new
                                                    {
                                                        x.its,
                                                        x.i,
                                                        x.it,
                                                        x.its.QuantityMin,
                                                        x.its.QuantityMax
                                                    })
                                                    .ToList(),
                                    }).ToListAsync();

                var groupedResult = result.Select(x => new
                {
                    TemplateStep = x.TemplateStep,
                    Ingredients = x.Ingredients
                                            .GroupBy(x => x.it)
                                            .Select(g => new IngredientTemplateStep
                                            {
                                                IngredientType = _mapper.Map<IngredientTypeDTO>(g.Key),
                                                items = g.Select(x => new GetIngredientTemplatStep
                                                {
                                                    Id = x.i.Id,
                                                    Name = x.i.Name,
                                                    Price = x.i.Price,
                                                    Calo = x.i.Calo,
                                                    Description = x.i.Description,
                                                    ImageUrl = x.i.ImageUrl
                                                }).ToList(),
                                                Min = g.FirstOrDefault()?.QuantityMin,
                                                Max = g.FirstOrDefault()?.QuantityMax
                                            }).ToList()
                }).ToList();

                return groupedResult.Select(x => new TemplateStepIngredientDTO
                {
                    TemplateStep = x.TemplateStep,
                    Ingredients = x.Ingredients,
                }).ToList();
            }
        
        public async Task<TemplateStepIngredientDTO> GetTemplateStepByTemplateStepId(int? templateStepId)
        {
            var result = await (from ts in _dbContext.TemplateSteps
                                where ts.Id == templateStepId
                                select new
                                {
                                    TemplateStep = _mapper.Map<TemplateStepDTO>(ts),
                                    Ingredients = _dbContext.IngredientTypeTemplateSteps
                                                .Where(its => its.TemplateStepId == ts.Id)
                                                .Join(_dbContext.Ingredients,
                                                      its => its.IngredientTypeId,
                                                      i => i.IngredientTypeId,
                                                      (its, i) => new { its, i })
                                                .Join(_dbContext.IngredientTypes,
                                                      x => x.i.IngredientTypeId,
                                                      it => it.Id,
                                                      (x, it) => new { x.its, x.i, it })
                                                .ToList(),
                                }).ToListAsync();

            var groupedResult = result.Select(x => new
            {
                TemplateStep = x.TemplateStep,
                Ingredients = x.Ingredients
                                        .GroupBy(x => x.it)
                                        .Select(g => new IngredientTemplateStep
                                        {
                                            IngredientType = _mapper.Map<IngredientTypeDTO>(g.Key),
                                            items = g.Select(x => new GetIngredientTemplatStep
                                            {
                                                Id = x.i.Id,
                                                Name = x.i.Name,
                                                Price = x.i.Price,
                                                Calo = x.i.Calo,
                                                Description = x.i.Description,
                                                ImageUrl = x.i.ImageUrl
                                            }).ToList()
                                        }).ToList()
            }).ToList();

            return groupedResult.Select(x => new TemplateStepIngredientDTO
            {
                TemplateStep = x.TemplateStep,
                Ingredients = x.Ingredients
            }).FirstOrDefault();
        }
    }
}

