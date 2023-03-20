using ChatEpt.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ChatEpt.Controllers;

public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly ApplicationContext _applicationContext;

    public MessageController(IMessageService messageService, ApplicationContext applicationContext)
    {
        _messageService = messageService;
        _applicationContext = applicationContext;
    }
    
    // POST, GET, DELETE, PUT, PATCH
    [HttpPost("api/messages")] // Attribute
    public IActionResult SendMessage(string message) // What is love 
    {
       
        // 1. Получить ответ от ИИ
        var result = _messageService.GetAnswer(message);
        
        // 2. Сохранить ответ с запросом в БД
        // TODO: Entity Framework
        
        /*
         * 200 - Ok(), NoContent()
         * 300 - info
         * 400 - BadRequest(), NotFound(), Forbid()
         * 500 - Problem()
         */
        return Ok(result);
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
}