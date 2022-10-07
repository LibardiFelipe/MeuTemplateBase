using System;

namespace TemplateBase.WebAPI.Models.Requests.Persons
{
    public class RegisterUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
