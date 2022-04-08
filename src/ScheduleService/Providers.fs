module WhereIsTheBus.ScheduleService.Providers

open FSharp.Data

type RouteProvider = HtmlProvider<"https://igis.ru/gortrans/bus/izh/29">

type ArrivalsProvider = HtmlProvider<"https://igis.ru/com/gortrans/page/online.php?nom=29&mode=1">

type StopsProvider = HtmlProvider<"https://igis.ru/gortrans/station/403">
