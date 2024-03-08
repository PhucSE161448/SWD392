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
        public async Task<IActionResult> GetNewsList([FromQuery]int? id = null ,[FromQuery] string? title = null)
        {
            var result = await _NewsService.GetAllNewsAsync(id, title);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNews(int id)
        {
            var result = await _NewsService.GetNewsAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNews([FromForm] AddNewsDTO createdNewsDTO)
        {
            string image = "";
            if (createdNewsDTO.formFile != null)
            {
                string fileName = createdNewsDTO.Title + Path.GetExtension(createdNewsDTO.formFile.FileName);
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
                image = baseUrl + "/ProductImage/" + fileName;
            }
            else
            {
                image = "https://placehold.co/600x400";
            }

            var result = await _NewsService.CreateNewsAsync(createdNewsDTO, image);
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
            string image = "";
            if (NewsDTO.formFile != null)
            {
                string fileName = NewsDTO.Title + Path.GetExtension(NewsDTO.formFile.FileName);
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
                image = baseUrl + "/ProductImage/" + fileName;
            }
            var result = await _NewsService.UpdateNewsAsync(id, NewsDTO, image);
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
