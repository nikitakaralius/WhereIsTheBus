namespace WhereIsTheBus.Application

open System

[<AutoOpen>]
module internal HelperFunctions =
    let withDigitsOnly text = text |> String.filter Char.IsDigit

    let isEmpty str = String.IsNullOrEmpty str

    let isNotEmpty str = isEmpty str = false
