using AutoMapper;
using TemplateBase.Application.Commands.Persons;
using TemplateBase.WebAPI.Models.Requests.Persons;

namespace TemplateBase.WebAPI.AutoMapper
{
    public class RequestsToCommands : Profile
    {
        public RequestsToCommands()
        {
            CreateMap<CreatePersonRequest, CreatePersonCommand>()
                .IncludeAllDerived()
                .AfterMap((_, command) => command.Validate());
        }
    }
}
