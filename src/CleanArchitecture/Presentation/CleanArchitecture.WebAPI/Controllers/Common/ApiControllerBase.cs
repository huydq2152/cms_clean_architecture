using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
       
    }
}
