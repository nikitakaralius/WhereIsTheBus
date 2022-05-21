namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.DTOs
open WhereIsTheBus.ScheduleService.StopsParser

[<ApiController>]
[<Route("/api/[controller]")>]
type StopsController() =
    inherit ControllerBase()

    [<HttpGet("{stopId:int}")>]
    member _.TransportBy(stopId: int) =
        task {
            try
                let! stopArrivals = asyncStopArrivals stopId
                return OkObjectResult(stopArrivals |> Seq.map(fun x -> x.Map())) :> IActionResult
            with
                _ -> return NotFoundResult()
        }
