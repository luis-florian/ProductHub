using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Database.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }
        public required Category Category { get; set; }

        // Navigation Properties
        public ICollection<Image>? Images { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
