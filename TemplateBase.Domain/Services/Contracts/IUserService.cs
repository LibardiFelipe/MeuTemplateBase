using System;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Entities;

namespace TemplateBase.Domain.Services.Contracts
{
    public interface IUserService : IService
    {
        Task<User> CreateUserAsync(string name, string email, string password, string profilePicture, DateTime birthDate, CancellationToken cancellationToken);
    }
}
