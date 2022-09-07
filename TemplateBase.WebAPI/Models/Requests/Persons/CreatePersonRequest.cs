using TemplateBase.Domain.Enumerators;

namespace TemplateBase.WebAPI.Models.Requests.Persons
{
    public class CreatePersonRequest
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public byte? Age { get; set; }
        public EPersonGenre? Genre { get; set; }
    }
}
