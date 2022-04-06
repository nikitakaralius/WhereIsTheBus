module internal WhereIsTheBus.ScheduleService.Dtos

open System
open WhereIsTheBus.Domain.Enums
open WhereIsTheBus.ScheduleService.InternalDomain

type SharedDirection = WhereIsTheBus.Domain.Enums.Direction

type RouteDto = {
    Transport: TransportType
    Number: int
    Direction: SharedDirection
}

let private toInternalTransport transportType =
    match transportType with
    | TransportType.Bus -> Transport.Bus
    | TransportType.Tram -> Transport.Tram
    | TransportType.Trolleybus -> Transport.Trolleybus
    | _ -> ArgumentOutOfRangeException(nameof transportType) |> raise
    
let private toInternalDirection direction =
    match direction with
    | SharedDirection.Direct -> Direction.Direct
    | SharedDirection.Return -> Direction.Return
    | SharedDirection.Both -> Direction.Both
    | _ -> ArgumentOutOfRangeException(nameof direction) |> raise

type RouteDto with
    member this.Domain() : TransportRoute =
        { Transport = this.Transport |> toInternalTransport
          Number = this.Number
          Direction = this.Direction |> toInternalDirection }