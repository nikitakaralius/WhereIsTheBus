const string clientName = "tgwebhook";

var builder = WebApplication.CreateBuilder(args);

var botConfiguration = builder.Configuration.OfBot();

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddHostedService<ConfigureWebhook>();
builder.Services.AddHttpClient(clientName)
                .AddTypedClient<ITelegramBotClient>(
              httpClient => new TelegramBotClient(botConfiguration.Token, httpClient));
builder.Services.AddHttpClient<IScheduleClient, HttpScheduleClient>();
builder.Services.AddScoped<IHandleUpdateService, HandleUpdateService>();
builder.Services.AddControllers()
                .AddNewtonsoftJson();

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
