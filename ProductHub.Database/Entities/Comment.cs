using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Database.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public required string Content { get; set; }

        public required int ProductId { get; set; }
        public Product? Product { get; set; }

        public required int UserId { get; set; }
        public User? User { get; set; }
    }
}
