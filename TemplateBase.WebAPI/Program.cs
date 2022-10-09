using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Application.Queries;
using TemplateBase.Application.Queries.Base;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Services;
using TemplateBase.Domain.Services.Contracts;
using TemplateBase.Infrastructure.Persistence.Contexts;
using TemplateBase.Infrastructure.UnitOfWork;
using TemplateBase.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    // Gera as rotas em minúsculo
    options.LowercaseUrls = true;
});

// Controllers
builder.Services.AddControllers();

// Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("TokenSecret").Value);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

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

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITemplateEmailService, TemplateEmailService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
