using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using tag_h.Injection.Controllers;

namespace tag_h.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ApiControllerBinderController : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public IEnumerable<ApiControllerBinder.ControllerDefinition> Get()
        {
            return ApiControllerBinder.GetControllerDefinitions();
        }

    }
}
