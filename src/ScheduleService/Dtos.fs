module WhereIsTheBus.ScheduleService.Dtos

open System
open WhereIsTheBus.ScheduleService.Types

type TransportDto =
    | Bus = 0
    | Trolleybus = 1
    | Tram = 2

type DirectionDto =
    | Direct = 0
    | Return = 1

type RouteDto =
    { Transport: TransportDto
      Number: int
      Direction: DirectionDto }

let toDomainTransport dto =
    match dto with
    | TransportDto.Bus -> Transport.Bus
    | TransportDto.Trolleybus -> Transport.Trolleybus
    | TransportDto.Tram -> Transport.Tram
    | _ -> ArgumentOutOfRangeException() |> raise

let toDomainDirection dto =
    match dto with
    | DirectionDto.Direct -> Direction.Direct
    | DirectionDto.Return -> Direction.Return
    | _ -> ArgumentOutOfRangeException() |> raise

type RouteDto with
    member this.Domain() : Route =
        { Transport = this.Transport |> toDomainTransport
          Number = this.Number
          Direction = this.Direction |> toDomainDirection }
