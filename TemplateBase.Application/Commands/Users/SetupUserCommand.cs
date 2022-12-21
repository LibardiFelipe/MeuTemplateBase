using System.Collections.Generic;
using System.Security.Claims;
using TemplateBase.Application.Commands.Base;

namespace TemplateBase.Application.Commands.Users
{
    public class SetupUserCommand : Command
    {
        public SetupUserCommand(IEnumerable<Claim> claims)
        {
            Claims = claims;
        }

        public IEnumerable<Claim> Claims { get; private set; }

        public override void Validate()
        {

        }
    }
}
