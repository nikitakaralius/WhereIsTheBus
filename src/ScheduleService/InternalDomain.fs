module WhereIsTheBus.ScheduleService.InternalDomain

type TransportType =
    | Bus
    | Trolleybus
    | Tram

type Direction =
    | Direct
    | Return
    | Both

type Arrival = { StopId: int; TimeToArrive: int }

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

type Route = {
    Name: string
    TimeToArrive: TimeToArrive
}

type Transport = {
    Name: string
    Routes: Route seq
}