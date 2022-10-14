using TemplateBase.Application.Commands.Base;

namespace TemplateBase.Application.Commands.Auth
{
    public class VerifyUserCommand : Command
    {
        public VerifyUserCommand()
        {

        }

        public VerifyUserCommand(string hash)
        {
            Hash = hash;
        }

        public string Hash { get; set; }

        public override void Validate()
        {
        }
    }
}
