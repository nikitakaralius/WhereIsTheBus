namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.DTOs
open WhereIsTheBus.ScheduleService.CachedArrivalsParser
open System.Linq

[<ApiController>]
[<Route("/api/[controller]")>]
type TransportController() =
    inherit ControllerBase()

    [<HttpPost>]
    member _.StopsOf(dto: TransportRouteDto) =
        task {
            try
                let! schedule = dto.Map() |> cachedSchedule
                return OkObjectResult (schedule.Select(fun x -> x.Map())) :> IActionResult
            with
            _ -> return NotFoundResult()
        }
