using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.TemplatesEmail;
using TemplateBase.Application.Queries.TemplatesEmail;
using TemplateBase.WebAPI.Models.Requests.TemplatesEmail;
using TemplateBase.WebAPI.Models.ViewModels;

namespace TemplateBase.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TemplatesEmailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TemplatesEmailController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTemplateEmailAsync([FromBody] CreateTemplateEmailRequest request)
        {
            var command = _mapper.Map<CreateTemplateEmailCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTemplateEmailAsync([FromBody] UpdateTemplateEmailRequest request)
        {
            var command = _mapper.Map<UpdateTemplateEmailCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] FilterTemplateEmailRequest request)
        {
            var query = _mapper.Map<TemplateEmailQuery>(request);
            var result = await _mediator.Send(query);
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            var query = new TemplateEmailQuery(id);
            var result = await _mediator.Send(query);
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
        {
            var command = new DeleteEmailTemplateCommand(id);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<ResultViewModel>(result);

            return response.Success
                ? Ok(response)
                : BadRequest(response);
        }
    }
}
