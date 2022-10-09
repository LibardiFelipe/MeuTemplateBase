using System;

namespace TemplateBase.WebAPI.Models.Requests.Users
{
    public class RegisterUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        // public IFormFile ProfilePicture { get; set; }
    }
}
