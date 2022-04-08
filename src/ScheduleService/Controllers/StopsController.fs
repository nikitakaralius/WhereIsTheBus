namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.StopsParser

[<ApiController>]
[<Route("/api/[controller]")>]
type Stops() =
    inherit ControllerBase()

    [<HttpGet>]
    member _.TransportBy(stopId: int) =
        task {
            if stopId > 0 then
                let! transport = asyncStopTransport stopId
                return OkObjectResult(transport) :> IActionResult
            else
                return NotFoundResult()
        }
