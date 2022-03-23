module WhereIsTheBus.ScheduleService.Parser

open System.Text.RegularExpressions
open System.Threading.Tasks
open FSharp.Data
open System.Linq
open WhereIsTheBus.ScheduleService.Providers
open WhereIsTheBus.ScheduleService.Domain
open WhereIsTheBus.ScheduleService.RouteUrlBuilder

let private transportStopsRegex = Regex "(?<='\\[)(.*)(?=\\]')"

let private arrivalsRegex = Regex "(?<=<small class=grey>)(.*)(?=</small>)"

let private parseArrivals (queryContent: HtmlDocument) =
    queryContent.ToString().Split('\n')
    |> Seq.map (fun x -> transportStopsRegex.Match x |> string |> withDigitsOnly,
                         arrivalsRegex.Match x |> string |> withDigitsOnly)
    |> Seq.where (fun (x, _) -> x |> isNotEmpty)
    |> Seq.map (fun (x, y) -> {
        StopId = x |> int
        TimeToArrive = (if y |> isEmpty then 0 else y |> int)
    })

let private parseStops (table: HtmlNode) =
    table.Descendants "a"
    |> Seq.choose
        (fun x ->
            x.TryGetAttribute("href")
            |> Option.map (fun a -> {
                Id = a.Value() |> withDigitsOnly |> int
                Name = x.InnerText()
                TimeToArrive = 0
            }))

let private applyArrivalToStop (stop: Stop) (arrival: Arrival) = { stop with TimeToArrive = arrival.TimeToArrive }

let private arrivalId arrival = arrival.StopId

let private stopId stop = stop.Id;

let asyncArrivals url =
    task {
        let! document = ArrivalsProvider.AsyncLoad url
        return document.Html |> parseArrivals
    }

let directRoute url =
    task {
        let! document = RouteStopsProvider.AsyncLoad url
        return document.Tables.Table8.Html |> parseStops
    }

let returnRoute url =
    task {
        let! document = RouteStopsProvider.AsyncLoad url
        return document.Tables.Table7.Html |> parseStops
    }

let mergeWith (arrivals: seq<Arrival>) (stops: seq<Stop>) = stops.Join(arrivals, stopId, arrivalId, applyArrivalToStop)

let asyncScheduleOf (routeStops: string -> Task<seq<Stop>>) stopsUrl arrivalsUrl =
    task {
        let! arrivals = asyncArrivals arrivalsUrl
        let! stops = routeStops stopsUrl
        return stops |> mergeWith arrivals 
    }

let private direction route =
    match route.Direction with
    | Direct -> directRoute
    | Return -> returnRoute

let schedule route = asyncScheduleOf (route |> direction) (route |> stopsUrl) (route |> arrivalsUrl)
