using AutoMapper;
using ProductHub.Database.Entities;
using ProductHub.Model.Dto;

namespace ProductHub.Middleware
{
    public class DtosProfile : Profile
    {
        public DtosProfile() 
        {
            // Category 

            this.CreateMap<Category, CreateCategoryDto>();
            this.CreateMap<CreateCategoryDto, Category>();

            this.CreateMap<Category, CategoryDto>();
            this.CreateMap<CategoryDto, Category>();

            this.CreateMap<Category, UpdateCategoryDto>();
            this.CreateMap<UpdateCategoryDto, Category>();

            // User

            this.CreateMap<User, CreateUserDto>();
            this.CreateMap<CreateUserDto, User>();

            this.CreateMap<User, UserDto>();
            this.CreateMap<UserDto, User>();

            this.CreateMap<User, UpdateUserDto>();
            this.CreateMap<UpdateUserDto, User>();
        }
    }
}
