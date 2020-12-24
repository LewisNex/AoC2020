namespace AoC2020.Code

open System.Text.RegularExpressions
open System.Runtime.InteropServices

module Utilities =
    
    let MatchInstructionString(pattern, instruction, [<Out>]out: byref<string>) : bool = 
        let rmatch = Regex.Match(instruction, pattern) in
            match rmatch.Success with
            | false -> false
            | true -> 
                out <- rmatch.Groups.["val"].Value
                true

    let MatchInstructionStringPair(pattern, instruction, [<Out>]out: byref<string * string>) : bool = 
        let rmatch = Regex.Match(instruction, pattern) in
            match rmatch.Success with
            | false -> false
            | true -> 
                out <- (rmatch.Groups.["val"].Value, rmatch.Groups.["val2"].Value)
                true

    let MatchInstruction(pattern, instruction, [<Out>]out: byref<int>) : bool = 
        let mutable outString = ""
        let result = MatchInstructionString(pattern, instruction, &outString)
        out <- outString |> int
        result
