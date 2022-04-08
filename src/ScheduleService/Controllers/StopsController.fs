namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.Dtos
open WhereIsTheBus.ScheduleService.StopsParser

[<ApiController>]
[<Route("/api/[controller]")>]
type Stops() =
    inherit ControllerBase()

    [<HttpGet("{stopId:int}")>]
    member _.TransportBy(stopId: int) =
        task {
            if stopId > 0 then
                let! transport = asyncStopTransport stopId
                return OkObjectResult(transport |> toTransportDtos) :> IActionResult
            else
                return NotFoundResult()
        }
