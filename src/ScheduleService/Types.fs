module WhereIsTheBus.ScheduleService.Types

type TransportType =
    | Undefined
    | Bus
    | Trolleybus
    | Tram

type Direction =
    | Direct
    | Return
    | Both

type StopTime = { StopId: int; TimeToArrive: int }

type TransportStop =
    { Id: int
      Name: string
      Direction: Direction
      TimeToArrive: int }

type TransportRoute =
    { Transport: TransportType
      Number: int
      Direction: Direction }

type TimeToArrive =
    | Minutes of int
    | Unspecified of string

type Arrival =
    { TransportNumber: int
      TimeToArrive: TimeToArrive }

type StopArrivals =
    { TransportType: TransportType
      Arrivals: Arrival seq }
