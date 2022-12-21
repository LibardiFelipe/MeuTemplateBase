﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Text.RegularExpressions;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Application.Queries;
using TemplateBase.Application.Queries.Base;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Services;
using TemplateBase.Domain.Services.Contracts;
using TemplateBase.Infrastructure.UnitOfWork;

namespace TemplateBase.WebAPI.IoC
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            return service;
        }

        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            service.AddMediatR(typeof(Query<>).Assembly, typeof(QueryHandler).Assembly);
            service.AddMediatR(typeof(Command).Assembly, typeof(CommandHandler).Assembly);
            
            service.AddScoped<ITemplateEmailService, TemplateEmailService>();
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
