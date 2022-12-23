using TemplateBase.Domain.Enumerators;
using TemplateBase.WebAPI.Models.Requests.Base;

namespace TemplateBase.WebAPI.Models.Requests.Users
{
    public class UserFilterRequest : FilterBase
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public EUserType? Type { get; set; }
    }
}
