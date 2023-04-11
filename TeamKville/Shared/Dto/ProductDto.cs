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
	    public int Id { get; set; }
	    public string Name { get; set; }
	    public string Description { get; set; }
	    public decimal Price { get; set; }
	    public bool IsActive { get; set; }
	    public int Age { get; set; }

	    public CategoryDto Category { get; set; }

		public GenreDto Genre { get; set; }

	    public IEnumerable<CommentDto>? Comments { get; set; }


	}
}
