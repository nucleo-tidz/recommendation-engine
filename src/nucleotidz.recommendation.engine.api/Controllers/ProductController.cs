using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace nucleotidz.recommendation.engine.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        /// <summary>
        /// Returns All the product 
        /// </summary>
        public async Task<IActionResult> Get()
        {
            await Task.CompletedTask;
            return Ok();
        }
    }
}
