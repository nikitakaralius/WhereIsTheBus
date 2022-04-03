var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<ConfigureWebhook>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
