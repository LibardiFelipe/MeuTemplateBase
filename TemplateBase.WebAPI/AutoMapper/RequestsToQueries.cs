using AutoMapper;
using TemplateBase.Application.Queries.Users;
using TemplateBase.WebAPI.Models.Requests.Persons;

namespace TemplateBase.WebAPI.AutoMapper
{
    public class RequestsToQueries : Profile
    {
        public RequestsToQueries()
        {
            CreateMap<FilterUserRequest, UserQuery>()
                .ConstructUsing((src, ctx) =>
                {
                    return new UserQuery()
                    .FilterByName(src.Name)
                    .FilterByEmail(src.Email)
                    .FilterByType(src.Type)
                    .FilterByBirthDate(src.BirthDate);
                });
        }
    }
}
