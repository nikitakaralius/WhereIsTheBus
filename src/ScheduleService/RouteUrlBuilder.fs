module WhereIsTheBus.ScheduleService.RouteUrlBuilder

open System
open WhereIsTheBus.ScheduleService.Types

let private host = "igis.ru"

let private transport route =
    match route.Transport with
    | Bus -> "bus/izh"
    | Trolleybus -> "trol"
    | Tram -> "tram"

let private mode route =
    match route.Transport with
    | Bus -> "1"
    | Trolleybus -> "2"
    | Tram -> "4"

let stopsUrlOf route =
    UriBuilder()
        .UseHttps()
        .WithHost(host)
        .WithPathSegment("gortrans")
        .WithPathSegment(route |> transport)
        .WithPathSegment(route.Number |> string)
        .Uri

let arrivalsUrlOf route =
    UriBuilder()
        .UseHttps()
        .WithHost(host)
        .WithPathSegment("com/page/online.php")
        .WithParameter("nom", route.Number |> string)
        .WithParameter("mode", route |> mode)
        .Uri
