using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3ITEST.UTILITES;

namespace S3ITEST.API.Controllers
{
    [Route("[controller]")]
    public class GraphQLController : ControllerBase
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExcutor;
        public GraphQLController(ISchema schema,IDocumentExecuter documentExcutor)
        {
            _schema = schema;
            _documentExcutor = documentExcutor;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            if (query ==null)
            {
                throw new ArgumentException(nameof(query));
            }

            var inputs = query.Variables?.ToInputs();
            var exucutionOptions = new ExecutionOptions()
            {
                Schema = _schema,
                Query = query.Query,
                Inputs = inputs
            };
            var result = await _documentExcutor.ExecuteAsync(exucutionOptions);
            if (result.Errors?.Count>0)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }

           
        }
    }
}