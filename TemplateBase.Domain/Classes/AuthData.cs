using System;
using TemplateBase.Domain.Enumerators;

namespace TemplateBase.Domain.Entities.Classes
{
    public class AuthData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public EUserType Type { get; set; }
        public string Token { get; set; }
    }
}
