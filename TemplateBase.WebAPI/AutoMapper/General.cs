using AutoMapper;
using Microsoft.OpenApi.Writers;
using System.Collections.Generic;
using System.Linq;
using TemplateBase.Application.Models;
using TemplateBase.Domain.Entities.Base;
using TemplateBase.WebAPI.Models.ViewModels;

namespace TemplateBase.WebAPI.AutoMapper
{
    public class General : Profile
    {
        public General()
        {
            CreateMap<Result, ResultViewModel>();
        }
    }
}
