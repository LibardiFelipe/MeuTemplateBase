using Flunt.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Entities;
using TemplateBase.Domain.Entities.Classes;
using TemplateBase.Domain.Resources;
using TemplateBase.Domain.Services.Contracts;
using TemplateBase.Domain.Specifications;
using TemplateBase.Domain.Utils;

namespace TemplateBase.Domain.Services
{
    public class AuthenticationService : Notifiable<Notification>, IAuthenticationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        public async Task<AuthData> AuthenticateAsync(string email, string password, CancellationToken cancellationToken)
        {
            var userRepo = _uow.Repository<User>();
            var user = (await userRepo.GetAllAsync(UserSpec.From(x => x.Email == email), cancellationToken)).FirstOrDefault();

            if (user == null || Hasher.Verify(password, user.Password) is false)
            {
                AddNotification("User", DefaultMessages.EmailOuSenhaIncorretos);
                return null;
            }

            return new AuthData
            {
                Name = user.Name,
                Email = user.Email,
                Permission = user.Permission,
                Token = GenerateToken(user)
        };
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("TokenSecret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Permission.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public IReadOnlyCollection<Notification> GetNotifications() => Notifications;
        public bool IsInvalid() => Notifications.Any();
    }
}
