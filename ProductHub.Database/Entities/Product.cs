using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public required int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<Image>? Images { get; set; }
        public ICollection<Comment>? Comments { get; set; }

        [NotMapped]
        public Image? Image { get; set; }
    }
}
