namespace AoC2020.Code

open System

module TimeTables = 
    let HowLongToWait(timestamp: int, id: int) =
        let mutable rem = 0
        let div = Math.DivRem(timestamp, id, &rem)
        id - rem

    let GetEarliestBus (busses: int seq) timestamp = 
        let map = Seq.fold (fun (map: Map<int, int>) (id: int) -> map.Add(id,(HowLongToWait(timestamp, id)))) Map.empty busses in
        Map.fold (fun (id, min) k v -> 
            match (k, v) with
            | (k, v) when v < min -> (k, v)
            | _ -> (id, min)) (0, 9999999) map