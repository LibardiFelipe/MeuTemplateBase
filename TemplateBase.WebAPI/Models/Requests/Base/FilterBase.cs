using System;

namespace TemplateBase.WebAPI.Models.Requests.Base
{
    public class FilterBase
    {
        public DateTime? CreatedAtStart { get; set; }
        public DateTime? CreatedAtEnd { get; set; }
        public DateTime? UpdatedAtStart { get; set; }
        public DateTime? UpdatedAtEnd { get; set; }
    }
}
