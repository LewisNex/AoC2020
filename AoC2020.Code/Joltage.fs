namespace AoC2020.Code

open Microsoft.FSharp.Collections

open System.Text.RegularExpressions

module Joltage =
    type Adapter = 
        struct
            val Rating: int
            val mutable Input: int option
            val mutable Output: int option
            new(rating) = {Rating = rating; Input = None; Output = None}
        end

    let AddOrIncrementMap (map: Map<int, int>) x = 
        match map.ContainsKey x with
        | true -> map.Add(x, map.[x] + 1)
        | false -> map.Add(x, 1)

    let SortAdapters (adapters: int seq) = Seq.sort adapters

    let CountDifferences (chain: int seq) = Seq.fold2 (fun map (x: int) (y: int) -> AddOrIncrementMap map (y - x)) Map.empty chain (Seq.tail chain)

    let GetJoltageJumps xs = Seq.append [0] xs |> Seq.append [Seq.max xs + 3] |>  SortAdapters |> CountDifferences

    let GetOutOptions y xs = Seq.filter (fun x -> x - y > 0 && x - y <= 3) xs

    let GetOptions (xs: int seq) = 
        let sorted = Seq.append [0] xs |> Seq.append [Seq.max xs + 3] |>  SortAdapters
        let differences = Seq.fold2 (fun acc (x: int) (y: int) -> Seq.append acc (Seq.toList [(y - x)])) Seq.empty sorted (Seq.tail sorted)
        let word = Seq.map string differences |> String.concat ""
        let matches = Regex.Matches(word, "(?<val>1+)3+") in
            matches 
            |> Seq.cast<Match> 
            |> Seq.map (fun x -> x.Groups.["val"].Value.Length) 
            |> Seq.filter (fun x -> x > 1)
            |> Seq.map (fun x -> 
                match x with
                | 4 -> 7
                | 3 -> 4
                | 2 -> 2
                | x -> System.Exception (sprintf "x") |> raise)
            |> Seq.reduce (fun acc x -> acc*x)
            //|> Seq.fold (fun acc x -> acc*x) 1 
