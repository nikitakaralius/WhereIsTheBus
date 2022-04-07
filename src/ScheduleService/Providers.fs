module WhereIsTheBus.ScheduleService.Providers

open FSharp.Data

type RouteStopsProvider = HtmlProvider<"https://igis.ru/gortrans/bus/izh/29">

type ArrivalsProvider = HtmlProvider<"https://igis.ru/com/gortrans/page/online.php?nom=29&mode=1">

type TransportStopsProvider = HtmlProvider<"https://igis.ru/gortrans/station/403">