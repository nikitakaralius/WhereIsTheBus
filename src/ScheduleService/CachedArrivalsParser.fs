module internal WhereIsTheBus.ScheduleService.CachedArrivalsParser

open System.Collections.Generic
open System.Linq
open InternalDomain
open ArrivalsParser
open RouteUrlBuilder

let private stops = Dictionary<TransportRoute, IEnumerable<TransportStop>>()

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
