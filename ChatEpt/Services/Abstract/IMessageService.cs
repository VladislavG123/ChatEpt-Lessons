using ChatEpt.DTOs;

namespace ChatEpt.Services.Abstract;

public interface IMessageService
{
    MessageServiceDto GetAnswer(string request);
}