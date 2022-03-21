module WhereIsTheBus.ScheduleService.Types

type Arrival = { StopId: int; TimeToArrive: int }

type Stop = { Id: int; Name: string; TimeToArrive: int }

type Transport =
    | Bus
    | Trolleybus
    | Tram

type Direction =
    | Direct
    | Return

type Route = { Type: Transport; Number: int; Direction: Direction  }
