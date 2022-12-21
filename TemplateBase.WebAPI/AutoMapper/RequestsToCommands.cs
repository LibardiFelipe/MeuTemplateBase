using AutoMapper;
using TemplateBase.Application.Commands.TemplatesEmail;
using TemplateBase.WebAPI.Models.Requests.TemplatesEmail;

namespace TemplateBase.WebAPI.AutoMapper
{
    public class RequestsToCommands : Profile
    {
        public RequestsToCommands()
        {
            CreateMap<CreateTemplateEmailRequest, CreateTemplateEmailCommand>()
                .IncludeAllDerived()
                .AfterMap((_, command) => command.Validate());

            CreateMap<UpdateTemplateEmailRequest, UpdateTemplateEmailCommand>()
                .IncludeAllDerived()
                .AfterMap((_, command) => command.Validate());
        }
    }
}
