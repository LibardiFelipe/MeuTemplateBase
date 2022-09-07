﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TemplateBase.Application.Commands.Persons;
using TemplateBase.WebAPI.Models.Requests.Persons;

namespace TemplateBase.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PersonController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonAsync([FromBody] CreatePersonRequest request)
        {
            var command = _mapper.Map<CreatePersonCommand>(request);
            var result = await _mediator.Send(command);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }
    }
}