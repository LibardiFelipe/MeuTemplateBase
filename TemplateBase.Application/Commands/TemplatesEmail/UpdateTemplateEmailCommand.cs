using System;
using TemplateBase.Application.Commands.Base;

namespace TemplateBase.Application.Commands.TemplatesEmail
{
    public class UpdateTemplateEmailCommand : Command
    {
        public Guid TemplateEmailId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }

        public override void Validate()
        {
        }
    }
}
