using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamKville.Shared.Models
{
    public class CreateProductModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public int GenreId { get; set; }

        public bool IsActive { get; set; }

        public int Age { get; set; }

    }
}
