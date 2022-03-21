namespace ScheduleService

#nowarn "20"

open System.Text.Json.Serialization
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services
            .AddControllers()
            .AddJsonOptions(fun options -> options.JsonSerializerOptions.Converters.Add(
                JsonFSharpConverter(
                    unionTagCaseInsensitive = true,
                    unionEncoding = JsonUnionEncoding.UnwrapFieldlessTags)))
            
        let app = builder.Build()

        app.UseHttpsRedirection()

        app.UseAuthorization()
        app.MapControllers()

        app.Run()
        
        exitCode
