using TemplateBase.Application.Commands.Base;

namespace TemplateBase.Application.Commands.Login
{
    public class AuthenticationCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override void Validate()
        {
        }
    }
}
