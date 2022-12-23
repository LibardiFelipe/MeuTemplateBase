using AutoMapper;
using TemplateBase.Application.Queries.Users;
using TemplateBase.WebAPI.Models.Requests.Users;

namespace TemplateBase.WebAPI.AutoMapper
{
    public class RequestsToQueries : Profile
    {
        public RequestsToQueries()
        {
            CreateMap<UserFilterRequest, UserQuery>()
                .ConstructUsing((src, ctx) =>
                {
                    return new UserQuery()
                    .FilterByName(src.Name)
                    .FilterByEmail(src.Email)
                    .FilterByType(src.Type)
                    .FilterByRangeCreation(src.CreatedAtStart, src.CreatedAtEnd)
                    .FilterByRangeUpdate(src.UpdatedAtStart, src.UpdatedAtEnd);
                })
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .AfterMap((_, src) => src.Validate());
        }
    }
}
