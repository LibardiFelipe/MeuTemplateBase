using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Entities.Classes;

namespace TemplateBase.Domain.Services.Contracts
{
    public interface IAuthenticationService : IService
    {
        Task<AuthData> AuthenticateAsync(string email, string password, CancellationToken cancellationToken);
    }
}
