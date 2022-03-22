namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.Parser
open WhereIsTheBus.ScheduleService.Types

[<ApiController>]
[<Route("/api/[controller]")>]
type TransportController() =
    inherit ControllerBase()

    [<HttpGet>]
    member _.StopsOf(route: Route) =
        task {
            try
                let! schedule = route |> schedule 
                return Results.Ok schedule
            with
            _ -> return Results.NotFound()
        }
