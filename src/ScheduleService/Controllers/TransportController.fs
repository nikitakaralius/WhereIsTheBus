namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.Dtos
open WhereIsTheBus.ScheduleService.Parser
open WhereIsTheBus.ScheduleService.RouteUrlBuilder

[<ApiController>]
[<Route("/api/[controller]")>]
type TransportController() =
    inherit ControllerBase()

    [<HttpGet>]
    member _.StopsOf(dto: RouteDto) =
        task {
            try
                let route = dto.Domain()
                let! stops = asyncScheduleOf directRoute (route |> stopsUrl) (route |> arrivalsUrl)
                return OkObjectResult stops :> IActionResult
            with
            _ -> return NotFoundResult()
        }
