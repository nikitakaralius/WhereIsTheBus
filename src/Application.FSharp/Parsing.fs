namespace WhereIsTheBus.Application

open System.Text.RegularExpressions
open FSharp.Data
open WhereIsTheBus.Domain.Entities
open WhereIsTheBus.Domain.ValueObjects

module internal Parsing =
    let private transportsStopRegex = Regex "(?<='\\[)(.*)(?=\\]')"
    let private arrivalRegex = Regex "(?<=<small class=grey>)(.*)(?=</small>)"

    let parseAjaxSchedule (ajax: HtmlDocument) =
        ajax.ToString().Split()
        |> Seq.map (fun x -> transportsStopRegex.Match(x) |> string, arrivalRegex.Match(x) |> string)
        |> Seq.map (fun (x, y) -> x |> withDigitsOnly, y |> withDigitsOnly)
        |> Seq.where (fun (x, _) -> x |> isNotEmpty)
        |> Seq.map (fun (x, y) -> x |> int, (if y |> isEmpty then 0 else y |> int))

    let parseRouteTable (table: HtmlNode) =
        table.Descendants "a"
        |> Seq.choose
            (fun x ->
                x.TryGetAttribute("href")
                |> Option.map (fun a -> a.Value() |> withDigitsOnly |> int, x.InnerText()))

    let arrivalsAsync url =
        task {
            let! document = Arrivals.AsyncLoad(url)
            return document.Html |> parseAjaxSchedule
        }

    let directStations url =
        task {
            let! document = RouteStations.AsyncLoad(url)
            return document.Tables.Table7.Html
                   |> parseRouteTable
        }

    let returnStations url =
        task {
            let! document = RouteStations.AsyncLoad(url)
            return document.Tables.Table8.Html
                   |> parseRouteTable
        }

    let toStation  (station: int * string) (arrival : int * int) =
        Station(fst arrival, snd station, Arrival(snd arrival))
