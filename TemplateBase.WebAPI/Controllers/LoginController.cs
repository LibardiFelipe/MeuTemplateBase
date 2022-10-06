using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Login;
using TemplateBase.WebAPI.Models.Requests.Login;
using TemplateBase.WebAPI.Models.ViewModels;

namespace TemplateBase.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LoginController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var command = _mapper.Map<AuthenticationCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }
    }
}
