using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.Store;
using Restaurant.Application.ViewModels.StoreDTO;

namespace Restaurant.WebAPI.Controllers.Stores
{
    public class StoreController : BaseController
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetStoreList([FromQuery] int ? id = null , [FromQuery] string? name = null)
        {
            var result = await _storeService.GetAllStoreAsync(id, name);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStore([FromForm] AddStoreDTO AddStoreDTO)
        {
            var result = await _storeService.CreateStoreAsync(AddStoreDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromForm] AddStoreDTO StoreDTO)
        {
            var result = await _storeService.UpdateStoreAsync(id, StoreDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var result = await _storeService.DeleteStoreAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
