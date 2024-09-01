using Microsoft.AspNetCore.Mvc;
using nucleotidz.recommendation.service.Interfaces;

namespace nucleotidz.recommendation.engine.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class OrderController(IOrderService orderService) : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create(string productCode, string customerEmail)
        {
            _ = orderService.Create(productCode, customerEmail);
            return Created();
        }
    }
}
