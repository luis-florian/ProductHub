using ProductHub.Database.Entities;

namespace ProductHub.Model.Dto
{
    public class ImageDto
    {
        public int Id { get; set; }
        public required string Url { get; set; }
    }

    public class CreateImageDto
    {
        public required string Url { get; set; }
        public int ProductId { get; set; }
    }
    public class DeleteImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ProductId { get; set; }
    }
}
