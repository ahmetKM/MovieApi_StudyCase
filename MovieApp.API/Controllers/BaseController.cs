using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.DTOs;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response) 
        {
            return new ObjectResult(response.StatusCode == 204 ? null : response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
