namespace ProductHub.Model.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
    public class CreateUserDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
