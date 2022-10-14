using System;

namespace TemplateBase.WebAPI.Models.Requests.TemplatesEmail
{
    public class UpdateTemplateEmailRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
    }
}
