using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamKville.Shared.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        //public Catergory category { get; set; }

        //TEMPORÄR KLASS SÅ JAG KAN EXPERIMENTERA
        public string Genre { get; set; }

        public string Category { get; set; }
        public bool IsActive { get; set; }

        public int Age { get; set; }

        public int Rating { get; set; }

        //public List<Comment> Comments { get; set; }
    }
}
