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
    
let private chunkByTransport (table: StopsProvider.Table6.Row[]) =
    let mutable transport: StopArrivals array = [||]
    let mutable routes: Arrival seq = []
    
    let updateRoutes() =
        if transport |> Array.isEmpty then
            ()
        else
            let index = transport.Length - 1
            transport[index] <- { transport[index] with Arrivals = routes }
            routes <- []
    
    for row in table do
        if knownTransportTypes |> List.contains row.Column1 then
            updateRoutes()
            let newTransport =
                {
                    Name = row.Column1
                    Routes = []
                }
            transport <- [|newTransport|] |> Array.append transport
        else
            let timeToArrive = match tryParseInt row.Column2 with
                               | Some value -> Minutes(value)
                               | None -> Unspecified(row.Column2)
            let route =
                {
                    TransportNumber = row.Column1 |> int
                    TimeToArrive = timeToArrive
                }
            routes <- [route] |> Seq.append routes
    updateRoutes()
    transport

let private exclude (transport: string list) (from: StopArrivals array) =
    from
    |> Array.filter(fun x -> not (transport |> List.contains x.Name))

let asyncStopTransport stopId =
    task {
        let! table = stopId |> stopUrl |> asyncStopTable
        let transport = table |> chunkByTransport
        return transport |> exclude ["Маршрутки"; "Пригородный автобус"]
    }