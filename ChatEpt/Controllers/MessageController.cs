using ChatEpt.DTOs;
using ChatEpt.Models;
using ChatEpt.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ChatEpt.Controllers;

public class MessageController : ControllerBase
{
    private readonly IAiService _aiService;
    private readonly ApplicationContext _applicationContext;
    private readonly IBadWordChecker _badWordChecker;

    public MessageController(IAiService aiService, ApplicationContext applicationContext,
        IBadWordChecker badWordChecker)
    {
        _aiService = aiService;
        _applicationContext = applicationContext;
        _badWordChecker = badWordChecker;
    }

    // POST, GET, DELETE, PUT, PATCH
    [HttpPost("api/messages")] // Attribute
    public IActionResult SendMessage(string message)
    {
        var fromDb = _applicationContext.Messages.FirstOrDefault(x => x.Request.Equals(message));
        if (fromDb is not null && fromDb.AllowSameRequest)
        {
            fromDb.RequestedCount++;
            _applicationContext.SaveChanges();
            return Ok(fromDb.Response);
        }

        var result = _badWordChecker.HasBadWordInText(message)
            ? new MessageServiceDto(message, "Please do not use bad words!")
            : _aiService.GetAnswer(message);

        if (result.NeedToSave)
        {
            _applicationContext.Messages.Add(new MessageEntity
            {
                Request = result.Request,
                Response = result.Answer
            });
            _applicationContext.SaveChanges();
        }
        
        return Ok(result.Answer);
    }

    [HttpGet("api/messages")]
    public IActionResult GetSavedMessages()
    {
        var messages = _applicationContext.Messages.Select(message => new
        {
            message.Request, message.Response
        }).ToList();

        return Ok(messages);
    }

    [HttpGet("api/messages/{request}")]
    public IActionResult GetAnswerByRequest(string request)
    {
        var message = _applicationContext.Messages.FirstOrDefault(x
            => x.Request.Equals(request, StringComparison.InvariantCultureIgnoreCase));

        return message is null
            ? NotFound($"There is no such message with request={request.Quoted()}")
            : Ok(message.Response);
    }
}