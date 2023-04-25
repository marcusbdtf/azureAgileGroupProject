namespace TeamKville.Shared.Dto;

public class EmailDto
{
    public string Header { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime TimeSent { get; set; }
    public string Name { get; set; } = string.Empty;
}