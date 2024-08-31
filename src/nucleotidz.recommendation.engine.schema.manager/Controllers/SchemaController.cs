using Microsoft.AspNetCore.Mvc;

namespace nucleotidz.recommendation.engine.schema.manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SchemaController(ISchemaManager schemaManager) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create()
        {
            await schemaManager.CreateCollection(true);
            return Created();
        }

    }
}
