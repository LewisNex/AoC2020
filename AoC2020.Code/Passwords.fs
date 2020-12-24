namespace AoC2020.Code

open System.Text.RegularExpressions

module Passwords =
    type Policy = Count | Position

    type PasswordEntry = 
        struct
            val Min: int
            val Max: int
            val Char: char
            val Password: string
            new(min, max, char, password) = {Min = min; Max = max; Char = char; Password = password}

            member this.IsValid policy : bool =
                match policy with
                | Count ->
                    let target = this.Char
                    let count = this.Password |> Seq.filter (function x -> x = target) |> Seq.length in
                    count >= this.Min && count <= this.Max
                | Position ->  
                    // Not zero indexed
                    let min = this.Min - 1
                    let max = this.Max - 1
                    (this.Password.[min] = this.Char) <> (this.Password.[max] = this.Char)
        end
        
    let GetValueFromCapture (capture: Group) = 
        match capture.Success with
        | true -> capture.Value
        | false -> raise (new System.Exception("No match found"))

    let ParseInput input = 
        Regex.Matches(input, "^(?<min>\d+)-(?<max>\d+) (?<char>\w): (?<password>\w+)$")
        |> Seq.cast<Match>
        |> Seq.head
        |> (function x -> x.Groups)
        |> (function x -> new PasswordEntry(
                                    (x.["min"] |> GetValueFromCapture |> int), 
                                    (x.["max"] |> GetValueFromCapture |> int),
                                    (x.["char"] |> GetValueFromCapture |> char),
                                    (x.["password"] |> GetValueFromCapture)
        ))

    let CountValid (inputs: string list) (policy: Policy) : int = 
        inputs 
        |> Seq.map ParseInput 
        |> Seq.filter (function x -> x.IsValid policy) 
        |> Seq.length