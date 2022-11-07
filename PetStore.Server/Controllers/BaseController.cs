using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PetStore.Server.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    public abstract class BaseController : ControllerBase
    {
        private ISender _mediator;
        private IMapper _mapper;

        /// <summary>
        /// Mediator dispatcher
        /// </summary>
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
        /// <summary>
        /// Global mapper
        /// </summary>
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
    }
}
