using System;
using TemplateBase.Domain.Enumerators;

namespace TemplateBase.WebAPI.Models.Requests.Persons
{
    public class FilterUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public EUserPermission? Permission { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
