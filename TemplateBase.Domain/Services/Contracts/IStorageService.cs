using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TemplateBase.Domain.Services.Contracts
{
    public interface IStorageService
    {
        Task<string> UploadImage(IFormFile formFile, CancellationToken cancellationToken);
    }
}
