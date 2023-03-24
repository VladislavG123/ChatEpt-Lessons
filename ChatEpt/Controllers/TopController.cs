using Microsoft.AspNetCore.Mvc;

namespace ChatEpt.Controllers;

public class TopController : ControllerBase
{
    private readonly ApplicationContext _context;

    public TopController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet("api/top/answers")]
    public ActionResult GetAnswersTop([FromQuery] int count) =>
        Ok(_context.Messages
            .GroupBy(x => x.Response)
            .Select(x => new { Answer = x.Key, x.ToList().Count })
            .OrderByDescending(x => x.Count)
            .Take(count)
            .ToList());

    [HttpGet("api/top/requests")]
    public IActionResult GetRequestTop([FromQuery] int count) =>
        Ok(_context.Messages
            .OrderByDescending(x => x.RequestedCount)
            .Take(count)
            .Select(x => new
            {
                x.Request, x.RequestedCount
            })
            .ToList());
}