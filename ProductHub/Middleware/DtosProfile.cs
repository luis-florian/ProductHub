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

            // Product

            this.CreateMap<Product, CreateProductDto>();
            this.CreateMap<CreateProductDto, Product>();

            this.CreateMap<Product, ProductDto>()
                .ForMember(p => p.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(p => p.Comments, opt => opt.MapFrom(src => src.Comments));
            this.CreateMap<Category, ProductDto>();
            this.CreateMap<Comment, ProductDto>();
            this.CreateMap<ProductDto, Product>();

            this.CreateMap<Product, UpdateProductDto>();
            this.CreateMap<UpdateProductDto, Product>();

            // Comment

            this.CreateMap<Comment, CreateCommentDto>();
            this.CreateMap<CreateCommentDto, Comment>();

            this.CreateMap<Comment, CommentDto>();
            this.CreateMap<CommentDto, Comment>();
            this.CreateMap<User, CommentDto>();


        }
    }
}
