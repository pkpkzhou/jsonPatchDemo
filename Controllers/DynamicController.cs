using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace JsonPatchSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicController : ControllerBase
    {
        [HttpPatch]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult PatchDynamic([FromBody] JsonPatchDocument patch)
        {
            dynamic obj = new ExpandoObject();

            patch.ApplyTo(obj);

            return Ok(obj);
        }
    }
}
