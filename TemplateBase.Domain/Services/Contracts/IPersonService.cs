using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Enumerators;

namespace TemplateBase.Domain.Services.Contracts
{
    public interface IPersonService : IService
    {
        Task<Person?> CreatePersonAsync(string? name, string? surname, string? email, byte? age, EPersonGenre? genre, CancellationToken cancellationToken);
    }
}
