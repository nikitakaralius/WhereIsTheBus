namespace WhereIsTheBus.Application

open Parsing
open System.Linq

type RouteParser =
    {
        StationsUrl: string
        ArrivalsUrl: string
    }

    member this.DirectRouteScheduleAsync() =
        this.ScheduleOf directStations

    member this.ReturnRouteScheduleAsync() =
        this.ScheduleOf returnStations

    member this.ScheduleOf directionFunction =
        task {
            let! arrival = arrivalsAsync this.StationsUrl
            let! routeStations = directionFunction this.StationsUrl
            return routeStations.Join(arrival, fst, fst, toStation)
        }


