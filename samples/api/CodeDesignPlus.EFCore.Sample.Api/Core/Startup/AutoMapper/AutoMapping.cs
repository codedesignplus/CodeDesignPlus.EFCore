using AutoMapper;
using CodeDesignPlus.Core.Models.Pager;
using CodeDesignPlus.EFCore.Sample.Api.Dto;
using CodeDesignPlus.EFCore.Sample.Api.Entities;

namespace CodeDesignPlus.EFCore.Sample.Api.Core.Startup.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap(typeof(Pager<>), typeof(Pager<>));
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
