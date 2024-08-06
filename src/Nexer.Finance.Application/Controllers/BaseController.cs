using Microsoft.AspNetCore.Mvc;
using Nexer.Finance.Domain.Commands;
using Nexer.Finance.Shared.Commands;

namespace Nexer.Finance.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleResponse(ICommandResult response)
        {
            if (response.Success)
            {
                return Ok(response);
            }

            if (response is CommandResponseErrors)
            {
                return BadRequest(response);
            }

            var responseError = (CommandResponseError)response;

            if (responseError.Message.Equals("Not found"))
                return NotFound(responseError);

            return BadRequest(response);
        }

        protected IActionResult HandleResponse<T>(T response)
        {
            if (response is null)
                return NotFound();

            return Ok(response);
        }
    }
}
