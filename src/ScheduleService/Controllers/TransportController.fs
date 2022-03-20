namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.Parser

[<ApiController>]
[<Route("/api/[controller]")>]
type TransportController() =
    inherit ControllerBase()

    [<HttpGet("{bus:int}")>]
    member _.StopsOf(bus: int) =
        task {
            try
                let! stops = asyncScheduleOf directRoute
                                 $"https://igis.ru/gortrans/bus/izh/{bus}"
                                 $"https://igis.ru/com/gortrans/page/online.php?nom={bus}&mode=1"
                return OkObjectResult stops :> IActionResult
            with
            _ -> return NotFoundResult()
        }
