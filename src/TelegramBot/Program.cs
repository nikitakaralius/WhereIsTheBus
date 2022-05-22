using WhereIsTheBus.TelegramBot.Extensions;

const string clientName = "tgwebhook";

var builder = WebApplication.CreateBuilder(args);
var botConfiguration = builder.Configuration.OfBot();

builder.Services.AddTelegramBot(clientName, botConfiguration);

var app = builder.Build();

app.UseRouting();
app.UseCors();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: clientName,
        pattern: $"bot/{botConfiguration.Token}",
        new { controller = "Webhook", action = "Post"});

    endpoints.MapControllers();
});

app.Run();
