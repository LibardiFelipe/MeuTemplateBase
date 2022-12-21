using AutoMapper;
using TemplateBase.Application.Queries.TemplatesEmail;
using TemplateBase.WebAPI.Models.Requests.TemplatesEmail;

namespace TemplateBase.WebAPI.AutoMapper
{
    public class RequestsToQueries : Profile
    {
        public RequestsToQueries()
        {
            CreateMap<FilterTemplateEmailRequest, TemplateEmailQuery>()
                .ConstructUsing((src, ctx) =>
                {
                    return new TemplateEmailQuery();
                });
        }
    }
}
