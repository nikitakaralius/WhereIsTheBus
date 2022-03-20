module WhereIsTheBus.ScheduleService.Parser

open System.Text.RegularExpressions
open FSharp.Data
open WhereIsTheBus.ScheduleService.Providers
open WhereIsTheBus.ScheduleService.Types

let private transportStopsRegex = Regex "(?<='\\[)(.*)(?=\\]')"

let private arrivalsRegex = Regex "(?<=<small class=grey>)(.*)(?=</small>)"

let private parseArrivals (queryContent: HtmlDocument) =
    queryContent.ToString().Split()
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
            }))

let asyncArrivals url =
    task {
        let! document = ArrivalsProvider.AsyncLoad url
        return document.Html |> parseArrivals
    }

let asyncDirectRouteStops url =
    task {
        let! document = RouteStationsProvider.AsyncLoad url
        return document.Tables.Table8.Html |> parseStops
    }

let asyncReturnRouteStops url =
    task {
        let! document = RouteStationsProvider.AsyncLoad url
        return document.Tables.Table7.Html |> parseStops
    }
