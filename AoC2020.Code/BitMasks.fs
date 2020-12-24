namespace AoC2020.Code

open System

module BitMasks =
    let NumberTo34BitString (n: int64) = 
        let string64 = Convert.ToString(n, 2).PadLeft(64, '0')
        string64.Substring(28)

    let ApplyMask (mask34: string) (numberString34:string) = 
        Seq.zip mask34 numberString34 
        |> Seq.map (fun (mask, bit ) -> 
            match mask with
            | 'X' -> bit
            | '1' -> '1'
            | '0' -> '0'
            | _ -> Exception "invalid mask" |> raise) 
        |> Seq.append (String.replicate 30 "0")
        |> String.Concat
        |> (fun x -> Convert.ToInt64(x, 2))
        

    let MaskNumber (mask34: string) (n: int64) = NumberTo34BitString n |> ApplyMask mask34

    type Instruction = UpdateMask of string | SetMemory of int64 * int64

    let ParseInstruction instruction = 
           let mutable out = ""
           let mutable out2 = "",""
           match instruction with
           | x when Utilities.MatchInstructionString(@"mask = (?<val>[1|0|X]+)", x, &out) -> UpdateMask out
           | x when Utilities.MatchInstructionStringPair(@"mem\[(?<val>\d+)\] = (?<val2>\d+)", x, &out2) -> 
                let address, value = out2 in
                    SetMemory (address |> int64, value |> int64)
           | _ ->  Exception "Invalid Instruction" |> raise

    type State = 
        struct
            val mutable Memory: Map<int64, int64>
            val mutable Mask: string
            new(mask) = {Memory = Map.empty; Mask = mask}

            member This.Process (inst: Instruction) = 
                match inst with 
                | UpdateMask x -> This.Mask <- x
                | SetMemory (address, value) ->
                    let masked = NumberTo34BitString value |> ApplyMask This.Mask in
                    This.Memory <- This.Memory.Add(address, masked)

            member This.SumMemory() = Map.fold(fun acc k v -> acc + v) 0L This.Memory
        end