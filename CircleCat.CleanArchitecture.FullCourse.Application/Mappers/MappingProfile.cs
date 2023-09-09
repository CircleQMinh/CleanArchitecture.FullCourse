using AutoMapper;
using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Category;
using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.Product;
using CircleCat.CleanArchitecture.FullCourse.Application.DTOs.User;
using CircleCat.CleanArchitecture.FullCourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleCat.CleanArchitecture.FullCourse.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<User, UserConfirmEmailDTO>().ReverseMap();
        }
    }
}
