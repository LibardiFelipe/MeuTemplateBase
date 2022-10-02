using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Application.Queries;
using TemplateBase.Application.Queries.Base;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Middlewares;
using TemplateBase.Domain.Services;
using TemplateBase.Domain.Services.Contracts;
using TemplateBase.Infrastructure.Persistence.Contexts;
using TemplateBase.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    // Gera as rotas em minúsculo
    options.LowercaseUrls = true;
});

// Controllers
builder.Services.AddControllers();

// DataContext
builder.Services.AddDbContext<DataContext>(x
    => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new NullReferenceException("A ConnectionString não foi encontrada!")));

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// MediatR
builder.Services.AddMediatR(typeof(Query<>).Assembly, typeof(QueryHandler).Assembly);
builder.Services.AddMediatR(typeof(Command).Assembly, typeof(CommandHandler).Assembly);

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();

// Middlewares
builder.Services.AddTransient<OperationCanceledMiddleware>();

//#if(EnableSwaggerSupport)
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//#endif
//#if(EnableSwaggerSupport)
builder.Services.AddSwaggerGen(option => {
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "TemplateBase", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, insira um token válido.",
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
//#endif
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
//#if(EnableSwaggerSupport)
    app.UseSwagger();
    app.UseSwaggerUI();
//#endif
}

// Middlewares
app.UseMiddleware<OperationCanceledMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
