namespace WhereIsTheBus.Application

open Parsing
open System.Linq

type RouteParser =
    {
        Url: string
    }

    member this.DirectRouteScheduleAsync() =
        this.ScheduleOf directStations

    member this.ReturnRouteScheduleAsync() =
        this.ScheduleOf returnStations

    member this.ScheduleOf directionFunction =
        task {
            let! arrival = arrivalsAsync this.Url
            let! routeStations = directionFunction this.Url
            return routeStations.Join(arrival, fst, fst, toStation)
        }


