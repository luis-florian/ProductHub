using ProductHub.Database.Entities;

namespace ProductHub.Model.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public required UserDto User { get; set; }
    }

    public class CreateCommentDto
    {
        public required string Content { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }

    public class DeleteCommentDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
    }
}
