namespace TeamKville.Server.Data.DataModels;

public class Order
{
	public int OrderId { get; set; }
	public ICollection<ProductQuantity> OrderedProducts { get; set; } = new List<ProductQuantity>();
	public string? UserId { get; set; }
	public DateTime OrderDate {get; set; }
	public bool Status { get; set; }
	public string Name { get; set; }
	public string Street { get; set; }
	public string City { get; set; }
	public string PostalCode { get; set; }
}