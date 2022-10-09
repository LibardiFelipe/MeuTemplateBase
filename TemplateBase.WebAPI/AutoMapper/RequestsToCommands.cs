using AutoMapper;
using TemplateBase.Application.Commands.TemplatesEmail;
using TemplateBase.Application.Commands.Login;
using TemplateBase.Application.Commands.Persons;
using TemplateBase.WebAPI.Models.Requests.TemplatesEmail;
using TemplateBase.WebAPI.Models.Requests.Login;
using TemplateBase.WebAPI.Models.Requests.Users;

namespace TemplateBase.WebAPI.AutoMapper
{
    public class RequestsToCommands : Profile
    {
        public RequestsToCommands()
        {
            CreateMap<RegisterUserRequest, RegisterUserCommand>()
                .IncludeAllDerived()
                .AfterMap((_, command) => command.Validate());

            CreateMap<UserLoginRequest, UserLoginCommand>()
                .IncludeAllDerived()
                .AfterMap((_, command) => command.Validate());

            CreateMap<CreateTemplateEmailRequest, CreateTemplateEmailCommand>()
                .IncludeAllDerived()
                .AfterMap((_, command) => command.Validate());
        }
    }
}
