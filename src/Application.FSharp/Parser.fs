namespace WhereIsTheBus.Application

open System.Text.RegularExpressions
open FSharp.Data

module internal Parser =
    let private transportsStopRegex = Regex "(?<='\\[)(.*)(?=\\]')"
    let private arrivalRegex = Regex "(?<=<small class=grey>)(.*)(?=</small>)"
    let private whiteSpace = "&nbsp;"

    let parseAjaxSchedule (ajax: HtmlDocument) =
        ajax.ToString().Split()
        |> Seq.map (fun x -> transportsStopRegex.Match(x) |> string, arrivalRegex.Match(x) |> string)
        |> Seq.map (fun (x, y) -> x |> withDigitsOnly, y.Replace(whiteSpace, " "))
        |> Seq.where (fun (x, _) -> x |> isNotEmpty)
        |> Seq.map (fun (x, y) -> x |> int, (if y |> isEmpty then "0 мин" else y))

    let parseRouteTable (table: HtmlNode) =
        table.Descendants "a"
        |> Seq.choose
            (fun x ->
                x.TryGetAttribute("href")
                |> Option.map (fun a -> a.Value() |> withDigitsOnly |> int, x.InnerText()))

    let scheduleAsync url =
        task {
            let! document = Schedule.AsyncLoad(url)
            return document.Html |> parseAjaxSchedule
        }

    let directRouteAsync url =
        task {
            let! document = Route.AsyncLoad(url)
            return document.Tables.Table7.Html
                   |> parseRouteTable
        }

    let returnRouteAsync url =
        task {
            let! document = Route.AsyncLoad(url)
            return document.Tables.Table8.Html
                   |> parseRouteTable
        }
