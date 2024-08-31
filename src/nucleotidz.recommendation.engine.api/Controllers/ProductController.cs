using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nucleotidz.recommendation.engine.api.Request.Product;
using nucleotidz.recommendation.service.Implementation;
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

        /// <summary>
        /// Returns All the product 
        /// </summary>
        public async Task<IActionResult> Get(string description)
        {
           await productService.Search(description); 
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(ProductCreateRequest productCreateRequest)
        {
            int totalCreated =await productService.Create(productCreateRequest.ToModel());
            return Created();
        }
    }
}
