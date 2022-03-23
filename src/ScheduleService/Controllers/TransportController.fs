namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.Dtos
open WhereIsTheBus.ScheduleService.Parser

[<ApiController>]
[<Route("/api/[controller]")>]
type TransportController() =
    inherit ControllerBase()

    [<HttpGet>]
    member _.StopsOf(dto: RouteDto) =
        task {
            try
                let! schedule = dto.Domain() |> schedule
                return Results.Ok schedule
            with
            _ -> return Results.NotFound()
        }
