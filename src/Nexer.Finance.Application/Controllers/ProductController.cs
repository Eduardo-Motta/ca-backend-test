using Microsoft.AspNetCore.Mvc;
using Nexer.Finance.Domain.Commands.Products;
using Shared.Handlers;

namespace Nexer.Finance.Application.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(ILogger<ProductController> logger)
        {

        }

        [HttpGet()]
        public async Task<IActionResult> FindAll([FromServices] IHandler<FindAllProductsCommand> handler, [FromQuery] FindAllProductsCommand command, CancellationToken cancellationToken)
        {
            var result = await handler.Handle(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindAll([FromServices] IHandler<FindProductByIdCommand> handler, [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new FindProductByIdCommand { Id = id };
            var result = await handler.Handle(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromServices] IHandler<CreateProductCommand> handler, [FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await handler.Handle(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromServices] IHandler<UpdateProductCommand> handler, [FromRoute] Guid id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
        {
            command.SetId(id);

            var result = await handler.Handle(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromServices] IHandler<DeleteProductCommand> handler, [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand { Id = id };
            var result = await handler.Handle(command, cancellationToken);

            return HandleResponse(result);
        }
    }
}
