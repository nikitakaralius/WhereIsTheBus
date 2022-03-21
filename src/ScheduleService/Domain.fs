module WhereIsTheBus.ScheduleService.Types

type Transport =
    | Bus
    | Trolleybus
    | Tram

type Direction =
    | Direct
    | Return

type Arrival = { StopId: int; TimeToArrive: int }

type Stop =
    { Id: int
      Name: string
      TimeToArrive: int }

type Route =
    { Transport: Transport
      Number: int
      Direction: Direction }
