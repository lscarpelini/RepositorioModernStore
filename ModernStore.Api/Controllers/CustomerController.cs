using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernStore.Domain.Commands.Handlers;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Infra.Transactions;
using System.Threading.Tasks;

namespace ModernStore.Api.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly CustomerCommadHandler _handler;

        public CustomerController(IUow ouw, CustomerCommadHandler handler) : base (ouw)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("v1/customers")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]RegisterCustomerCommad command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }
    }
}