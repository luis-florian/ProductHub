namespace ProductHub.Model
{
    public class FileUpload
    {
        public required IFormFile Files { get; set; }
        public int ProductId { get; set; }
    }
}
