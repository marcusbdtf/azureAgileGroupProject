using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamKville.Shared.Models
{
	public class AddProductToShoppingCartModel
	{
		public string UserId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
