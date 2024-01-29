using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.Database.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public required string Url { get; set; }

        public required int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
