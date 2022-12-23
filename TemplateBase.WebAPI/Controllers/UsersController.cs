using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Users;
using TemplateBase.Application.Queries.Users;
using TemplateBase.WebAPI.Models.Requests.Users;
using TemplateBase.WebAPI.Models.ViewModels;

namespace TemplateBase.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("setup")]
        public async Task<IActionResult> SetupUserAsync()
        {
            var command = new SetupUserCommand(User.Claims);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] string id)
        {
            var query = new UserQuery(id);
            var result = await _mediator.Send(query);
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] UserFilterRequest request)
        {
            var query = _mapper.Map<UserQuery>(request);
            var result = await _mediator.Send(query);
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }
    }
}
