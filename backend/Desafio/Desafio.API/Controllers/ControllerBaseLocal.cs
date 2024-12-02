using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ControllerBaseLocal : ControllerBase
    {
        private ISender _mediator;

        /// <summary>
        /// MediatR instance
        /// </summary>
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
