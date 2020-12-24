namespace AoC2020.Code

module Encoding =
    type PairResult = 
        NoPairs 
        | Pair of int64 * int64

    let rec GetPair sum (ls : int64 seq) : PairResult = 
        match ls |> Seq.toList with
        | [] -> NoPairs
        | _::[] -> NoPairs
        | x::xs -> 
            let y = sum - x in 
                match (Seq.exists (fun a -> a = y) xs) with
                    | true -> Pair(x, y)
                    | false -> GetPair sum xs

    let IsValid (x: int64) (checks: int64 list) =
        match GetPair x checks with
        | NoPairs -> false
        | _ -> true

    let FindInvalids n (xs: int64 seq) = 
        let preamble = (Seq.toArray xs).[0..n-1] |> Seq.toList
        let toCheck = (Seq.toArray xs).[n..] |> Seq.toList
        let rec FindInvalids ys checks =
            match ys with
            | [] -> []
            | y::rest -> 
                let nextChecks = Seq.append (Seq.tail checks) [y] |> Seq.toList
                match IsValid y checks with
                | true -> FindInvalids rest nextChecks
                | false -> y::(FindInvalids rest nextChecks)
        in FindInvalids toCheck preamble
            
    let GetWeaknesses target ys = 
        let rec GetWeaknesses (xs: int64 list) (weaknesses: int64 list) =
            match Seq.sum weaknesses with
            | sum when sum = target -> weaknesses
            | sum when sum > target -> GetWeaknesses ((Seq.append weaknesses xs) |> Seq.tail |> Seq.toList) []
            | sum when sum < target -> GetWeaknesses (Seq.tail xs |> Seq.toList) ((Seq.append weaknesses [Seq.head xs] |> Seq.toList))
        in GetWeaknesses (ys |> Seq.toList) []
