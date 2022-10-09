using System;

namespace TemplateBase.WebAPI.Models.Requests.TemplatesEmail
{
    public class UpdateTemplateEmailRequest
    {
        public TemplateEmailInUpdateEmailRequest TemplateEmail { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
    }

    public class TemplateEmailInUpdateEmailRequest
    {
        public Guid Id { get; set; }
    }
}
