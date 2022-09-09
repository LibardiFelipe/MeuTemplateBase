using System.Collections.Generic;

namespace TemplateBase.WebAPI.Models.ViewModels
{
    public class ResultViewModel
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
    }
}
