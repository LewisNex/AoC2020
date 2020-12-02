namespace AoC2020.Code


module ExpenseReport =
    
    type PairResult = 
        NoPairs 
        | Pair of int * int
    type TripleResult = 
        NoTriples
        | Triple of  int * int * int

    let rec GetPair sum (ls : int seq) : PairResult = 
        match ls |> Seq.toList with
        | [] -> NoPairs
        | _::[] -> NoPairs
        | x::xs -> 
            let y = sum - x in 
                match (Seq.exists (fun a -> a = y) xs) with
                    | true -> Pair(x, y)
                    | false -> GetPair sum xs

    let rec GetTriple sum (ls : int seq) = 
        match ls |> Seq.toList with
        | [] -> NoTriples
        | _::[] -> NoTriples
        | _::_::[] -> NoTriples
        | x::xs ->
            let rest = sum - x in
                match GetPair rest xs with
                | NoPairs -> GetTriple sum xs
                | Pair(y, z) -> Triple(x, y, z)

    let GetProductOfSummedPair sum (xs: int seq) = 
        match GetPair sum xs with
        | NoPairs -> raise (new System.Exception("No match found"))
        | Pair (x, y) -> x * y

    let GetProductOfSummedTriple sum (xs: int seq) = 
        match GetTriple sum xs with
        | NoTriples -> raise (new System.Exception("No match found"))
        | Triple (x, y, z) -> x * y * z