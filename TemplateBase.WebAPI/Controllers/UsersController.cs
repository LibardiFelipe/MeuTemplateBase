using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Persons;
using TemplateBase.Application.Queries.Users;
using TemplateBase.WebAPI.Models.Requests.Persons;

namespace TemplateBase.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] FilterUserRequest request)
        {
            var query = _mapper.Map<UserQuery>(request);
            var result = await _mediator.Send(query);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            var query = new UserQuery(id);
            var result = await _mediator.Send(query);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonAsync([FromBody] CreateUserRequest request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            var result = await _mediator.Send(command);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }
    }
}
