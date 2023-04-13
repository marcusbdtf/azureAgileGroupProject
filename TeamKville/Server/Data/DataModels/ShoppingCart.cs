namespace TeamKville.Server.Data.DataModels
{
	public class ShoppingCart
	{
		public int ShoppingCartId { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }

		public ICollection<CartItem> CartItems { get; set; }

	}
}
