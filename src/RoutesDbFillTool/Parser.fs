module WhereIsTheBus.RoutesDbFillTool.Parser

open System.Text.RegularExpressions
open System.Threading.Tasks
open FSharp.Data
open WhereIsTheBus.RoutesDbFillTool.Providers
open WhereIsTheBus.RoutesDbFillTool.Types
open WhereIsTheBus.RoutesDbFillTool.RouteUrlBuilder
open WhereIsTheBus.RoutesDbFillTool.HelperFunctions

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

let private merge (stop: Stop) (arrival: Arrival) = { stop with TimeToArrive = arrival.TimeToArrive }

let private arrivalId arrival = arrival.StopId

let private stopId stop = stop.Id;

let private asyncArrivals url =
    task {
        let! document = ArrivalsProvider.AsyncLoad url
        return document.Html |> parseArrivals
    }

let private directRoute url =
    task {
        let! document = RouteStationsProvider.AsyncLoad url
        return document.Tables.Table8.Html |> parseStops
    }

let private returnRoute url =
    task {
        let! document = RouteStationsProvider.AsyncLoad url
        return document.Tables.Table7.Html |> parseStops
    }

let private asyncScheduleOf (routeStops: string -> Task<seq<Stop>>) stopsUrl arrivalsUrl =
    task {
        let! arrivals = asyncArrivals arrivalsUrl
        let! stops = routeStops stopsUrl
        return stops.Join(arrivals, stopId, arrivalId, merge)
    }    

let private direction route =
    match route.Direction with
    | Direct -> directRoute
    | Return -> returnRoute

let schedule route = asyncScheduleOf (route |> direction) (route |> stopsUrl) (route |> arrivalsUrl)
