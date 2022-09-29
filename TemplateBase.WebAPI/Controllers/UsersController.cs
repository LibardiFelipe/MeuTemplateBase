using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Persons;
using TemplateBase.Application.Queries.Users;
using TemplateBase.WebAPI.Models.Requests.Persons;
using TemplateBase.WebAPI.Models.ViewModels;

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
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            var query = new UserQuery(id);
            var result = await _mediator.Send(query);
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }
    }
}
