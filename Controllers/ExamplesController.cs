using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace rest_api_custom_jwt_auth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExamplesController : ControllerBase
    {
        private static List<string> Examples;
        static ExamplesController()
        {
            Examples = new List<string>()
            {
                "Example 0",
                "Example 1",
                "Example 2"
            };
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetExamples()
        {
            return Ok(Examples);
        }

        [HttpGet("{exampleId:int}")]
        public IActionResult GetExampleById(int exampleId)
        {
            var element = Examples.ElementAtOrDefault(exampleId);
            if (element == null)
                return NotFound();

            return Ok(element);
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpDelete("{exampleId:int}")]
        public IActionResult DeleteExampleById(int exampleId)
        {
            if (Examples.ElementAtOrDefault(exampleId) is null)
                return NotFound();

            Examples.RemoveAt(exampleId);
            return NoContent();
        }
    }
}
