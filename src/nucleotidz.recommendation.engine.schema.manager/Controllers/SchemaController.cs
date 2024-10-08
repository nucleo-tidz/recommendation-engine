using Microsoft.AspNetCore.Mvc;
using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.engine.schema.manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SchemaController(ISchemaManager schemaManager, IVectorDatabaseHelper vectorDatabaseHelper) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<IActionResult> Create()
        {
            await schemaManager.CreateCollection(true);
            return Created();
        }

        [HttpPost("create/index")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(string collectionName, string indexName, string fieldName)
        {
            await schemaManager.CreateIndex(collectionName, indexName, fieldName);
            return Created();
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete()
        {
            await vectorDatabaseHelper.DropCollection();
            return NoContent();
        }
    }
}
