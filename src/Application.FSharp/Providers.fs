namespace WhereIsTheBus.Application

open FSharp.Data

[<AutoOpen>]
module internal InternalDomainTypes =
    type Route = HtmlProvider<"https://igis.ru/gortrans/bus/izh/29">

    type Schedule = HtmlProvider<"https://igis.ru/com/gortrans/page/online.php?nom=29&mode=1">
