using System.Net;

namespace TemplateBase.Domain.Classes
{
    public class EmailConfig
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
