using Microsoft.AspNetCore.Mvc;
using time_of_your_life.Filters;

namespace time_of_your_life.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}
