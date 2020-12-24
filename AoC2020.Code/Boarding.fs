namespace AoC2020.Code

open System
open System.Text.RegularExpressions

module Boarding = 
    type Direction = Positive | Negative

    let Partition (min:int) (max:int) direction : int*int = 
        let step = (min - max) |> Math.Abs |> function x -> (x/2 + 1) in
        match direction with
        | Negative -> (min, max - step)
        | Positive -> (min + step, max)

    let CalculateBinaryPartition top (partition: Direction list) : int = 
        let rec CalcRecBinaryPartition((min, max), input) : int = 
            match input with
            | [] -> min
            | x::[] -> 
                match x with
                | Positive -> max
                | Negative -> min
            | x::xs -> CalcRecBinaryPartition(Partition min max x, xs)
        in CalcRecBinaryPartition((0, top), partition)


    let ParseSingleDirection c = 
        match c with 
        | 'L' -> Negative
        | 'R' -> Positive
        | 'F' -> Negative
        | 'B' -> Positive
        | _ -> raise (new System.Exception("Invalid direction"))

    let ParseBinaryPartition (input: string) = input |> Seq.map ParseSingleDirection |> Seq.toList

    let SplitIntoAxes input = 
        let groups = Regex.Match(input, "(?<fb>[F|B]+)(?<lr>[L|R]+)").Groups in
        (groups.["fb"].Value, groups.["lr"].Value)

    type BoardingPass = 
        struct 
            val Row: int
            val Column: int
            new(pass : string) = 
                let rows, cols = SplitIntoAxes pass in
                {Row = rows |> ParseBinaryPartition |> CalculateBinaryPartition 127;
                Column = cols |> ParseBinaryPartition |> CalculateBinaryPartition 7;}
                
            member This.SeatId = This.Row * 8 + This.Column

        end

    let FindMissingSeat (passes : BoardingPass list) = 
        let ids = passes |> Seq.map (fun x -> x.SeatId)
        let min = Seq.min ids
        let max = Seq.max ids
        [min..max] |> Seq.filter (fun x -> Seq.contains x ids |> not)