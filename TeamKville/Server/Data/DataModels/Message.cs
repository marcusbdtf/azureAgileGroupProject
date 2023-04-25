namespace TeamKville.Server.Data.DataModels;

public class Message
{
	public int Id { get; set; }
	public string Header { get; set; }
	public string Body { get; set; }
	public string Sender { get; set; }
	public bool IsRead { get; set; }
	public DateTime TimeSent { get; set; }
	public string SenderName { get; set; }

}