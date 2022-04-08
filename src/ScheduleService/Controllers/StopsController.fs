namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.Dtos
open WhereIsTheBus.ScheduleService.StopsParser

[<ApiController>]
[<Route("/api/[controller]")>]
type StopsController() =
    inherit ControllerBase()

    [<HttpGet("{stopId:int}")>]
    member _.TransportBy(stopId: int) =
        task {
            try
                let! transport = asyncStopTransport stopId
                return OkObjectResult(transport |> toTransportDtos) :> IActionResult
            with
                _ -> return NotFoundResult()
        }
