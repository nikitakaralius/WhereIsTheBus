module WhereIsTheBus.ScheduleService.CachedParser

open System.Collections.Generic
open System.Linq
open Domain
open Parser
open RouteUrlBuilder


let private stops = Dictionary<Route, IEnumerable<Stop>>()

let cachedSchedule route =
    match stops.TryGetValue route with
    | true, value when value.Any() -> task {
        let! arrivals = asyncArrivals (route |> arrivalsUrl)
        return value |> mergeWith arrivals
        }
    | _ ->
        task {
            let! schedule = route |> schedule
            stops.Add(route, schedule)
            return schedule
        }
