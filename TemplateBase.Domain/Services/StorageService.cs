using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Domain.Services
{
    public class StorageService : IStorageService
    {
        public StorageService()
        {
        }

        // Deve retornar o URL da imagem upada, ou vazio caso algum problema tenha ocorrido.
        public async Task<string> UploadImage(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile.Length <= 0)
                return "";

            var fileExtension = formFile.FileName[^4..];
            if (!fileExtension.Contains("jpg") && !fileExtension.Contains("png"))
                return "";

            var safeFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}{fileExtension}";

            // TODO: Fazer o upload da imagem
            throw new NotImplementedException();
        }
    }
}
