using Google.Apis.Util;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Text.RegularExpressions;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Application.Queries;
using TemplateBase.Application.Queries.Base;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Services;
using TemplateBase.Domain.Services.Contracts;
using TemplateBase.Infrastructure.Persistence.Contexts;
using TemplateBase.Infrastructure.UnitOfWork;

namespace TemplateBase.WebAPI.IoC
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
        {
            /* Database context */
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            service.AddDbContext<DataContext>(
                (serviceProvider, dbContextOptions) =>
                {
                    var configuration = serviceProvider.GetService<IConfiguration>();
                    var connectionString = configuration!.GetConnectionString("DefaultConnection");
                    connectionString.ThrowIfNullOrEmpty("ConnectionString (DefaultConnection)");

                    dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
                }
            );

            service.AddScoped<IUnitOfWork, UnitOfWork>();

            return service;
        }

        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            service.AddMediatR(typeof(Query<,>).Assembly, typeof(QueryHandler).Assembly);
            service.AddMediatR(typeof(Command).Assembly, typeof(CommandHandler).Assembly);
            
            service.AddScoped<IStorageService, StorageService>();
            service.AddScoped<IUserService, UserService>();

            service.AddControllers();

            return service;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option => {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "TemplateBaseAPI", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insert the token without the Bearer part.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .SetIsOriginAllowed((origin) =>
                        {
                            var localhost = LocalhostPattern().IsMatch(origin);
                            return localhost;
                        })
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders(new string[] { "content-type", "content-disposition", "content-lenght" });
                });
            });

            return services;
        }

        [GeneratedRegex("^https?:\\/\\/localhost(:[0-9]+)?")]
        private static partial Regex LocalhostPattern();
    }
}
