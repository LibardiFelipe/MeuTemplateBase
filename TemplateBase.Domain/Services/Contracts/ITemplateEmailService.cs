using System;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Entities;

namespace TemplateBase.Domain.Services.Contracts
{
    public interface ITemplateEmailService : IService
    {
        Task<TemplateEmail> CreateTemplateEmailAsync(string name, string body, CancellationToken cancellationToken);
        Task<TemplateEmail> UpdateTemplateEmailAsync(Guid id, string name, string body, CancellationToken cancellationToken); 
    }
}
