using AutoMapper;
using TemplateBase.Application.Queries.Persons;
using TemplateBase.WebAPI.Models.Requests.Persons;

namespace TemplateBase.WebAPI.AutoMapper
{
    public class RequestsToQueries : Profile
    {
        public RequestsToQueries()
        {
            CreateMap<FilterPersonRequest, PersonQuery>()
                .ConstructUsing((src, ctx) =>
                {
                    return new PersonQuery()
                    .FilterByName(src.Name)
                    .FilterBySurname(src.Surname)
                    .FilterByEmail(src.Email)
                    .FilterByAge(src.Age)
                    .FilterByGenre(src.Genre);
                });
        }
    }
}
