using ChatEpt.DTOs;

namespace ChatEpt.Services.Abstract;

public interface IAiMessageRouter
{
    /// <summary>
    /// An artificial intelligence answers to <param name="request"></param>
    /// </summary>
    /// <exception cref="BadHttpRequestException">Throws when status code is not success</exception>
    /// <returns>MessageServiceDto - dto with request and answer</returns>
    MessageDto GetAnswer(string request);
}