namespace TeamKville.Shared.Dto;

public class OrderDto
{
	public int OrderId { get; set; }
	public Guid UserId { get; set; }
	public DateTime OrderDate { get; set; }
	public bool Status { get; set; }
}