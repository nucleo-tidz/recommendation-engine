using Microsoft.AspNetCore.Mvc;
using nucleotidz.recommendation.model;
using nucleotidz.recommendation.service.Interfaces;
using nucleotidz.recommendation.engine.api.Response;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(string customerEmail)
        {
            IEnumerable<OrderEntity> data =await  orderService.Get(customerEmail);
            return Ok(data.ToOrderResponse());
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(string productCode, string customerEmail)
        {
            _ = await orderService.Delete(productCode, customerEmail);
            return NoContent();
        }

    }
}
