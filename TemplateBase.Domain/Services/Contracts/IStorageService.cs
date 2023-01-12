using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TemplateBase.Domain.Services.Contracts
{
    public interface IStorageService : IService
    {
        Task<string> UploadFile(IFormFile formFile, string[] allowedExtensions, CancellationToken cancellationToken);
    }
}
