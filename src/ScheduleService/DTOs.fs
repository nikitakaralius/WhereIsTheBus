module WhereIsTheBus.ScheduleService.DTOs

open WhereIsTheBus.ScheduleService.Types

type DirectionDto =
    | Undefined = 0
    | Direct = 1
    | Return = 2
    | Both = 3

type TransportTypeDto =
    | Undefined = 0
    | Bus = 1
    | Trolleybus = 2
    | Tram = 3

let private transportType (dto: TransportTypeDto) =
    match dto with
    | TransportTypeDto.Bus -> Bus
    | TransportTypeDto.Trolleybus -> Trolleybus
    | TransportTypeDto.Tram -> Tram
    | _ -> failwith "Invalid transport type"

let private direction (dto: DirectionDto) =
    match dto with
    | DirectionDto.Direct -> Direct
    | DirectionDto.Return -> Return
    | DirectionDto.Both -> Both
    | _ -> failwith "Invalid direction"

type TransportRouteDto =
    { TransportType: TransportTypeDto
      Number: int
      Direction: DirectionDto }

type TransportRouteDto with
    member this.Map() =
        { Transport = transportType this.TransportType
          Number = this.Number
          Direction = direction this.Direction }

type TransportStopDto =
    { Id: int
      Name: string
      Direction: DirectionDto
      TimeToArrive: int }

type TransportStopDto with
    member this.Map() : TransportStop =
        { Id = this.Id
          Name = this.Name
          Direction = direction this.Direction
          TimeToArrive = this.TimeToArrive }

type Direction with
    member this.Map() =
        match this with
        | Direct -> DirectionDto.Direct
        | Return -> DirectionDto.Return
        | Both -> DirectionDto.Both

type TransportType with
    member this.Map() =
        match this with
        | Bus -> TransportTypeDto.Bus
        | Trolleybus -> TransportTypeDto.Trolleybus
        | Tram -> TransportTypeDto.Trolleybus
        | _ -> TransportTypeDto.Undefined

type TransportStop with
    member this.Map() : TransportStopDto =
        { Id = this.Id
          Name = this.Name
          Direction = this.Direction.Map()
          TimeToArrive = this.TimeToArrive }

type ArrivalDto =
    { TransportNumber: int
      TimeToArrive: string }
    
type Arrival with
    member this.Map(): ArrivalDto =
        {
            TransportNumber = this.TransportNumber
            TimeToArrive = match this.TimeToArrive with
                           | Minutes t -> t |> string
                           | Unspecified s -> s
        }
        
type StopArrivalsDto = {
    Transport: TransportTypeDto
    Arrivals: ArrivalDto seq
}

type StopArrivals with
    member this.Map(): StopArrivalsDto =
        {
            Transport = this.TransportType.Map()
            Arrivals = this.Arrivals |> Seq.map(fun x -> x.Map())
        }