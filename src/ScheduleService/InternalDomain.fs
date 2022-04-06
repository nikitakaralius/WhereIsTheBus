module internal WhereIsTheBus.ScheduleService.InternalDomain

type Transport =
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
    { Transport: Transport
      Number: int
      Direction: Direction }
