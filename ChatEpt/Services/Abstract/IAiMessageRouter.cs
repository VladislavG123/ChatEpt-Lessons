using ChatEpt.DTOs;

namespace ChatEpt.Services.Abstract;

public interface IAiMessageRouter
{
    MessageDto GetAnswer(string request);
}