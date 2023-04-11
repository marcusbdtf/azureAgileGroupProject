using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamKville.Shared.Models
{
	public class CreateCommentModel
	{
		public string Name { get; set; }
		public string Text { get; set; }
		public int Rating { get; set; }

		public int ProductId { get; set; }
	}
}
