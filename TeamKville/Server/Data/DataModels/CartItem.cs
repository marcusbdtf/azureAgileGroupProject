﻿namespace TeamKville.Server.Data.DataModels
{
	public class CartItem
	{
		public int CartItemId { get; set; }

		public Product Product { get; set; }
		public int ProductId { get; set; }

		public int Quantity { get; set; }

		public ShoppingCart ShoppingCart { get; set; }
		public int ShoppingCartId { get; set; }
	}
}
