using System;
using TemplateBase.Application.Commands.Base;

namespace TemplateBase.Application.Commands.TemplatesEmail
{
    public class DeleteEmailTemplateCommand : Command
    {
        public DeleteEmailTemplateCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public override void Validate()
        {
        }
    }
}
