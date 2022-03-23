namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.Dtos
open WhereIsTheBus.ScheduleService.CachedParser

[<ApiController>]
[<Route("/api/[controller]")>]
type TransportController() =
    inherit ControllerBase()

    [<HttpGet>]
    member _.StopsOf(dto: RouteDto) =
        task {
            try
                let! schedule = dto.Domain() |> cachedSchedule
                return Results.Ok schedule
            with
            _ -> return Results.NotFound()
        }
