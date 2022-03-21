namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.Parser
open WhereIsTheBus.ScheduleService.RouteUrlBuilder
open WhereIsTheBus.ScheduleService.Types

[<ApiController>]
[<Route("/api/[controller]")>]
type TransportController() =
    inherit ControllerBase()

    [<HttpGet>]
    member _.StopsOf(route: Route) =
        task {
            try
                let! stops = asyncScheduleOf directRoute (route |> stopsUrl) (route |> arrivalsUrl)
                return OkObjectResult stops :> IActionResult
            with
            _ -> return NotFoundResult()
        }
