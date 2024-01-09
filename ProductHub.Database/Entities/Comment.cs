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

        public int ProductId { get; set; }
        public required Product Product { get; set; }

        public int? UserId { get; set; }
        public required User User { get; set; }
    }
}
