using ChatEpt.DTOs;

namespace ChatEpt.Services.Ai.Abstract;

public interface IAiService
{
    string[] KeyWords { get; }
    MessageDto GetAnswer(string request); 
}