using Microsoft.AspNetCore.Mvc;
using nucleotidz.recommendation.service.Interfaces;

namespace nucleotidz.recommendation.engine.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await productService.Get());
        }

        [HttpPost("bulk")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(IFormFile file)
        {
            _ = await productService.Create(file.OpenReadStream());
            return Created();
        }

        [HttpGet("suggest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string email)
        {
            await productService.Suggest(email);
            return Ok();
        }
    }
}
