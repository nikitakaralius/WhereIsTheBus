var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<ConfigureWebhook>();
builder.Services.AddHttpClient("tgwebhook")
       .AddTypedClient<ITelegramBotClient>(
              httpClient => new TelegramBotClient(builder.Configuration.OfBot().Token,
                                                 httpClient));
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
