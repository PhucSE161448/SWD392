using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
