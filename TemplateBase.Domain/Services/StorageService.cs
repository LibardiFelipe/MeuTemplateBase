using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Services.Contracts;

namespace TemplateBase.Domain.Services
{
    public class StorageService : Notifiable<Notification>, IStorageService
    {
        public StorageService()
        {
        }

        public async Task<string> UploadFile(IFormFile formFile, string[]? allowedExtensions, CancellationToken cancellationToken)
        {
            if (formFile is null)
            {
                AddNotification("FormFile", "FormFile é nulo!");
                return "";
            }

            if (formFile.Length <= 0)
                AddNotification("FormFile", "FormFile não tem conteúdo!");

            var fileExtension = formFile.FileName[^3..];
            if (allowedExtensions is not null)
                if (allowedExtensions.Contains(fileExtension) is false)
                    AddNotification("FormFile", "O arquivo possui uma extesão que não é permitida!");

            if (IsInvalid())
                return "";

            var safeFileName = $"{Guid.NewGuid():N}.{fileExtension}";

            // TODO: Implement the upload method
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Notification> GetNotifications() => Notifications;
        public bool IsInvalid() => Notifications.Any();
    }
}
