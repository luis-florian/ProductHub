using ProductHub.Database.Entities;

namespace ProductHub.Model.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public required CategoryDto Category { get; set; }
        public ICollection<CommentDto>? Comments { get; set; }
    }

    public class CreateProductDto
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }
    }
}
