namespace TeamKville.Shared.Dto;

public class OrderDto
{
	public int OrderId { get; set; }
	public ICollection<ProductQuantityDto> OrderedProductsDto { get; set; } = new List<ProductQuantityDto>();
	public string? UserId { get; set; }
	public DateTime OrderDate { get; set; }
	public bool Status { get; set; }
	public string Name { get; set; }
	public string Street { get; set; }
	public string City { get; set; }
	public string PostalCode { get; set; }
}