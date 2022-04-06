namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.Dtos
open WhereIsTheBus.ScheduleService.CachedParser
open System.Linq

[<ApiController>]
[<Route("/api/[controller]")>]
type internal TransportController() =
    inherit ControllerBase()

    [<HttpPost>]
    member _.StopsOf(dto: TransportRouteDto) =
        task {
            try
                let! schedule = dto.Domain() |> cachedSchedule
                return OkObjectResult (schedule.Select(fun x -> toDto x)) :> IActionResult
            with
            _ -> return NotFoundResult()
        }
