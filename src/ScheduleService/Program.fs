module WhereIsTheBus.ScheduleService.Program

#nowarn "20"

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

let exitCode = 0

let builder = WebApplication.CreateBuilder()

builder.Services.AddControllers()

let app = builder.Build()

app.MapControllers()
app.Run()

exitCode
