module WhereIsTheBus.ScheduleService.StopsParser

open Types
open Providers
open StopUrlBuilder

let knownTransportTypes = ["Автобус"; "Троллейбус"; "Трамвай"; "Маршрутки"; "Пригородный автобус"]

let private asyncStopTable url =
    task {
        let! stopDocument = StopsProvider.AsyncLoad(url)
        return stopDocument.Tables.Table6.Rows
               |> Array.filter(fun x -> x.Column1 <> "Номер маршрута")
    }
    
let private parse transport =
    match transport with
    | "Автобус" -> Bus
    | "Троллейбус" -> Trolleybus
    | "Трамвай" -> Tram
    | _ -> Undefined
    
let private chunkByTransport (table: StopsProvider.Table6.Row[]) =
    let mutable transport: StopArrivals array = [||]
    let mutable arrivals: Arrival seq = []
    
    let updateArrivals() =
        if transport |> Array.isEmpty then
            ()
        else
            let index = transport.Length - 1
            transport[index] <- { transport[index] with Arrivals = arrivals }
            arrivals <- []
    
    for row in table do
        if knownTransportTypes |> List.contains row.Column1 then
            updateArrivals()
            let newTransport =
                {
                    TransportType = parse row.Column1
                    Arrivals = []
                }
            transport <- [|newTransport|] |> Array.append transport
        else
            let timeToArrive = match tryParseInt row.Column2 with
                               | Some value -> Minutes(value)
                               | None -> Unspecified(row.Column2)
            let arrival =
                {
                    TransportNumber = row.Column1 |> int
                    TimeToArrive = timeToArrive
                }
            arrivals <- [arrival] |> Seq.append arrivals
    updateArrivals()
    transport

let private exclude (from: StopArrivals array) =
    from |> Array.filter(fun x -> x.TransportType <> Undefined)

let asyncStopArrivals stopId =
    task {
        let! table = stopId |> stopUrl |> asyncStopTable
        let transport = table |> chunkByTransport
        return transport
    }