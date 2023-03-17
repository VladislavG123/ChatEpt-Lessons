using ChatEpt.DTOs;

namespace ChatEpt.Services.Abstract;

public interface IMessageService
{
    /// <summary>
    /// An artificial intelligence answers to <param name="request"></param>
    /// </summary>
    /// <exception cref="BadHttpRequestException">Throws when status code is not success</exception>
    /// <returns>MessageServiceDto - dto with request and answer</returns>
    MessageServiceDto GetAnswer(string request);
}