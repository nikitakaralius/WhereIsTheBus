namespace WhereIsTheBus.ScheduleService.Controllers

open Microsoft.AspNetCore.Mvc
open WhereIsTheBus.ScheduleService.Parser

[<ApiController>]
[<Route("/api/[controller]")>]
type TransportController() =
    inherit ControllerBase()
    
    [<HttpGet>]
    member _.Get() =
        task {
            return! asyncDirectRouteStops "https://igis.ru/gortrans/bus/izh/29"
        }
