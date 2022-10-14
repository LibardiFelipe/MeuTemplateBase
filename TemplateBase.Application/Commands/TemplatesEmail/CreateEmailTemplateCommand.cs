using TemplateBase.Application.Commands.Base;

namespace TemplateBase.Application.Commands.TemplatesEmail
{
    public class CreateTemplateEmailCommand : Command
    {
        public string Name { get; set; }
        public string Body { get; set; }

        public override void Validate()
        {
        }
    }
}
