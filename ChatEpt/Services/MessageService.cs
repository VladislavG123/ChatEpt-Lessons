using System.Text;
using ChatEpt.DTOs;
using ChatEpt.Services.Abstract;

namespace ChatEpt.Services;

public class MessageService : IMessageService
{
    public MessageServiceDto GetAnswer(string request)
    {
        // Get answer from https://whatthecommit.com/index.txt
        // Return answer

        return new MessageServiceDto(request, "Some Answer");
    }
}