module WhereIsTheBus.ScheduleService.StopUrlBuilder

open System

let private host = "igis.ru"

let stopUrl (stopId: int) =
    UriBuilder()
        .UseHttps()
        .WithHost(host)
        .WithPathSegment("gortrans/station")
        .WithPathSegment($"{stopId}")
        .Uri
        .ToString()