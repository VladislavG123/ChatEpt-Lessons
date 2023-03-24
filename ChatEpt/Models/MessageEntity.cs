namespace ChatEpt.Models;

public class MessageEntity : Entity
{
    public string Request { get; set; }
    public string Response { get; set; }
    public uint RequestedCount { get; set; } = 1;
}