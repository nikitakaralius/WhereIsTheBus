module WhereIsTheBus.ScheduleService.Dtos

open System
open WhereIsTheBus.Domain.Enums
open WhereIsTheBus.ScheduleService.InternalDomain

type SharedDirection = WhereIsTheBus.Domain.Enums.Direction

type DomainTransportStop = WhereIsTheBus.Domain.Records.TransportStop

type DomainRoute = WhereIsTheBus.Domain.Records.Route

type TransportRouteDto = {
    Transport: TransportType
    Number: int
    Direction: SharedDirection
}

type TransportStopDto = {
    Id: int
    Name: string
    Direction: StrictDirection
    TimeToArrive: int
}

type TransportDto = {
    Name: string
    Routes: DomainRoute seq
}

let private toInternalTransport transportType =
    match transportType with
    | TransportType.Bus -> TransportType.Bus
    | TransportType.Tram -> TransportType.Tram
    | TransportType.Trolleybus -> TransportType.Trolleybus
 
let private toInternalDirection direction =
    match direction with
    | SharedDirection.Direct -> Direction.Direct
    | SharedDirection.Return -> Direction.Return
    | SharedDirection.Both -> Direction.Both
    | _ -> ArgumentOutOfRangeException(nameof direction) |> raise

let private toDomainDirection direction =
    match direction with
    | Direction.Direct -> SharedDirection.Direct
    | Direction.Return -> SharedDirection.Return
    | Direction.Both -> SharedDirection.Both

let private toDomainRoute (route: Route) =
    let timeToArrive =
        match route.TimeToArrive with
        | Minutes m -> m.ToString()
        | Unspecified s -> s
    DomainRoute(route.Number, timeToArrive)

let toDto (stop: TransportStop) =
    {
        Id = stop.Id
        Name = stop.Name
        Direction = (toDomainDirection stop.Direction).AsStrictDirection()
        TimeToArrive = stop.TimeToArrive
    }

let toTransportDtos (transport: seq<Transport>) =
    transport
    |> Seq.map(fun x ->
        {
            Name = x.Name
            Routes = x.Routes |> Seq.map toDomainRoute
        })

type TransportRouteDto with
    member this.Domain() : TransportRoute =
        { Transport = this.Transport |> toInternalTransport
          Number = this.Number
          Direction = this.Direction |> toInternalDirection }