module internal WhereIsTheBus.ScheduleService.ArrivalsParser

open System.Text.RegularExpressions
open System.Threading.Tasks
open FSharp.Data
open System.Linq
open WhereIsTheBus.ScheduleService.Providers
open WhereIsTheBus.ScheduleService.Types
open WhereIsTheBus.ScheduleService.RouteUrlBuilder

let private transportStopsRegex = Regex("(?<='\\[)(.*)(?=\\]')", RegexOptions.Compiled)

let private arrivalsRegex = Regex("(?<=<small class=grey>)(.*)(?=</small>)", RegexOptions.Compiled)

let private parseArrivals (queryContent: HtmlDocument) =
    queryContent.ToString().Split('\n')
    |> Seq.map (fun x -> transportStopsRegex.Match x |> string |> withDigitsOnly,
                         arrivalsRegex.Match x |> string |> withDigitsOnly)
    |> Seq.where (fun (x, _) -> x |> isNotEmpty)
    |> Seq.map (fun (x, y) -> {
        StopId = x |> int
        TimeToArrive = (if y |> isEmpty then 0 else y |> int)
    })

let private parseStops direction (table: HtmlNode) =
    table.Descendants "a"
    |> Seq.choose
        (fun x ->
            x.TryGetAttribute("href")
            |> Option.map (fun a -> {
                Id = a.Value() |> withDigitsOnly |> int
                Name = x.InnerText()
                TimeToArrive = 0
                Direction = direction
            }))

let private applyArrivalToStop (stop: TransportStop) (arrival: StopTime) = { stop with TimeToArrive = arrival.TimeToArrive }

let private arrivalId arrival = arrival.StopId

let private stopId stop = stop.Id;

let asyncArrivals url =
    task {
        let! document = ArrivalsProvider.AsyncLoad url
        return document.Html |> parseArrivals
    }

let directRoute url =
    task {
        let! document = RouteProvider.AsyncLoad url
        return document.Tables.Table8.Html |> parseStops Direction.Direct 
    }

let returnRoute url =
    task {
        let! document = RouteProvider.AsyncLoad url
        return document.Tables.Table7.Html |> parseStops Direction.Return
    }

let bothRoutes url =
    task {
        let! directRoute = directRoute url
        let! returnRoute = returnRoute url
        return directRoute |> Seq.append returnRoute
    }

let mergeWith (arrivals: seq<StopTime>) (stops: seq<TransportStop>) = stops.Join(arrivals, stopId, arrivalId, applyArrivalToStop)

let asyncScheduleOf (routeStops: string -> Task<seq<TransportStop>>) routeUrl arrivalsUrl =
    task {
        let! arrivals = asyncArrivals arrivalsUrl
        let! stops = routeStops routeUrl
        return stops |> mergeWith arrivals 
    }

let private direction route =
    match route.Direction with
    | Direct -> directRoute
    | Return -> returnRoute
    | Both   -> bothRoutes

let schedule route = asyncScheduleOf (route |> direction) (route |> routeUrl) (route |> arrivalsUrl)
