﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nucleotidz.recommendation.engine.api.Request.Product;
using nucleotidz.recommendation.infrastructure.Interfaces;
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
        public async Task<IActionResult> Get()
        {
            return Ok(await productService.Get());
        }

        [HttpPost("bulk")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(IFormFile file)
        {
            int totalCreated = await productService.Create(file.OpenReadStream());
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
