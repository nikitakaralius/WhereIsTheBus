namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.Types
open WhereIsTheBus.ScheduleService.Parser
open WhereIsTheBus.ScheduleService.RouteUrlBuilder

[<ApiController>]
[<Route("/api/[controller]")>]
type TransportController() =
    inherit ControllerBase()

    [<HttpGet>]
    member _.StopsOf(route: Route) =
        task {
            try
                let stopsUrl = route |> stopsUrl
                let arrivalsUrl = route |> arrivalsUrl
                let! stops = asyncScheduleOf directRoute stopsUrl arrivalsUrl
                return OkObjectResult stops :> IActionResult
            with
            _ -> return NotFoundResult()
        }
