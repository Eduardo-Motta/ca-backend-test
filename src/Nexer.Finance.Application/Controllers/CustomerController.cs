using Microsoft.AspNetCore.Mvc;
using Nexer.Finance.Domain.Commands.Customer;
using Shared.Handlers;

namespace Nexer.Finance.Application.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(ILogger<CustomerController> logger) { }

        [HttpGet()]
        public async Task<IActionResult> FindAll([FromServices] IHandler<FindAllCustomersCommand> handler, [FromQuery] FindAllCustomersCommand command, CancellationToken cancellationToken)
        {
            var result = await handler.Handle(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindAll([FromServices] IHandler<FindCustomerByIdCommand> handler, [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new FindCustomerByIdCommand { Id = id };
            var result = await handler.Handle(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromServices] IHandler<CreateCustomerCommand> handler, [FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var result = await handler.Handle(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromServices] IHandler<UpdateCustomerCommand> handler, [FromRoute] Guid id, [FromBody] UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            command.SetId(id);

            var result = await handler.Handle(command, cancellationToken);

            return HandleResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromServices] IHandler<DeleteCustomerCommand> handler, [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCustomerCommand { Id = id };
            var result = await handler.Handle(command, cancellationToken);

            return HandleResponse(result);
        }
    }
}
