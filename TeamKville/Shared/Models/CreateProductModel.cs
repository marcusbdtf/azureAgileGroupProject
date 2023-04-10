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

        //public Category List<Category> { get; set; }

        public bool IsActive { get; set; }

        public int Age { get; set; }

        public int Rating { get; set; }
    }
}
