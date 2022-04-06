namespace WhereIsTheBus.TelegramBot.Controllers;

public class WebhookController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] IHandleUpdateService handleUpdateService, 
                                          [FromBody] Update update)
    {
        await handleUpdateService.EchoAsync(update);
        return Ok();
    }
}