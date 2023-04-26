namespace TeamKville.Shared.Dto;

public class EmailDto
{
    public int Id { get; set; }
    public string Header { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime TimeSent { get; set; }
    public string SenderName { get; set; } = string.Empty;
}