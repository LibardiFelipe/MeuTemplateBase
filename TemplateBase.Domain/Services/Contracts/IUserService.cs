using System;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Entities;

namespace TemplateBase.Domain.Services.Contracts
{
    public interface IUserService : IService
    {
        Task<User> RegisterUserAsync(string name, string email, string password, DateTime birthDate, CancellationToken cancellationToken);
        Task<bool> VerifyUserAsync(string hash, CancellationToken cancellationToken);
    }
}
