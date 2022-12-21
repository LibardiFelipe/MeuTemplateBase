using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Entities;

namespace TemplateBase.Domain.Services.Contracts
{
    public interface IUserService : IService
    {
        Task<User?> SetupUserAsync(IEnumerable<Claim> claims, CancellationToken cancellationToken);
    }
}
