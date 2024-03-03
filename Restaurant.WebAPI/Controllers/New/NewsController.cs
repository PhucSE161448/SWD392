using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.News;
using Restaurant.Application.ViewModels.NewsDTO;

namespace Restaurant.WebAPI.Controllers.New
{
    public class NewsController : BaseController
    {
        private readonly INewsService _NewsService;
        public NewsController(INewsService NewsService)
        {
            _NewsService = NewsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetNewsList([FromQuery] string? description = null)
        {
            var result = await _NewsService.GetAllNewsAsync(description);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNews([FromForm] AddNewsDTO createdNewsDTO)
        {
            if (createdNewsDTO.Image != null)
            {
                string fileName = createdNewsDTO.Status + Path.GetExtension(createdNewsDTO.formFile.FileName);
                string filePath = @"wwwroot\ProductImage\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                FileInfo file = new FileInfo(directoryLocation);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    createdNewsDTO.formFile.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                createdNewsDTO.Image = baseUrl + "/ProductImage/" + fileName;
            }
            else
            {
                createdNewsDTO.Image = "https://placehold.co/600x400";
            }

            var result = await _NewsService.CreateNewsAsync(createdNewsDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        /*  [Authorize(Roles = "admin")]*/
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, [FromForm] AddNewsDTO NewsDTO)
        {
            if (NewsDTO.Image != null)
            {
                string fileName = NewsDTO.Status + Path.GetExtension(NewsDTO.formFile.FileName);
                string filePath = @"wwwroot\ProductImage\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                FileInfo file = new FileInfo(directoryLocation);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    NewsDTO.formFile.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                NewsDTO.Image = baseUrl + "/ProductImage/" + fileName;
            }
            var result = await _NewsService.UpdateNewsAsync(id, NewsDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        /* [Authorize(Roles = "admin")]*/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var result = await _NewsService.DeleteNewsAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
