using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TemplateBase.Application.Commands.Base;
using TemplateBase.Application.Queries;
using TemplateBase.Application.Queries.Base;
using TemplateBase.Domain.Contracts;
using TemplateBase.Domain.Services;
using TemplateBase.Domain.Services.Contracts;
using TemplateBase.Infrastructure.Persistence.Contexts;
using TemplateBase.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
builder.Services.AddScoped<IPersonService, PersonService>();

//#if(EnableSwaggerSupport)
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//#endif
//#if(EnableSwaggerSupport)
builder.Services.AddSwaggerGen();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
