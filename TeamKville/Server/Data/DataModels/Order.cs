namespace TeamKville.Server.Data.DataModels;

public class Order
{
	public int OrderId { get; set; }

	//public IEnumerable<ProductQuantity> OrderProducts { get; set; } = new List<ProductQuantity>();
	public Guid UserId { get; set; }
	public DateTime OrderDate {get; set; }
	public bool Status { get; set; }

}