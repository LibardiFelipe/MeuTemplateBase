using System.Collections.Generic;

namespace TemplateBase.Domain.Classes
{
    public class Email
    {
        private List<string> _addressees = new List<string>();

        public string Subject { get; set; }
        public string Body { get; set; }
        public IReadOnlyCollection<string> Addressees { get { return _addressees; } }

        public void InjectToBody(Dictionary<string, string> values)
        {
            if (string.IsNullOrWhiteSpace(Body))
                return;

            foreach (var item in values)
                Body = Body.Replace(item.Key, item.Value);
        }

        public void AddAddressee(string email)
        {
            _addressees.Add(email.Trim());
        }

        public void AddAddressee(List<string> emails)
        {
            foreach (var email in emails)
                _addressees.Add(email.Trim());
        }
    }
}
