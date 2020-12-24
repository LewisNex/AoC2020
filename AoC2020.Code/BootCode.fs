namespace AoC2020.Code

open System.Text.RegularExpressions
open System.Runtime.InteropServices

module Types =
    type Value = int
    type Operation = 
        ACC of Value 
        | JMP of Value 
        | NOP of Value

module Errors =
    exception OutOfRangeException of string * int
    exception InfiniteLoopException of string * int
    exception InvalidOperationException of string

module Computer =
    type Program =
        struct 
            val mutable Pointer: int
            val mutable Instructions: Types.Operation list
            val mutable Accumulator: int
            val mutable History: int list
            new(instructions) = {Pointer = 0; Instructions = instructions; Accumulator = 0; History = []}

            member This.IsPointerOutOfRange pointer = pointer < 0 || pointer >= This.Instructions.Length
            member This.IsOffsetOutOfRange offset = This.IsPointerOutOfRange (This.Pointer + offset)

            member This.GetOperationAtPointer pointer : Types.Operation = 
                match This.IsPointerOutOfRange pointer with
                | true ->  Errors.OutOfRangeException ((sprintf "Pointer %d is out of range" pointer), This.Accumulator) |> raise
                | false -> This.Instructions.[pointer]

            member This.GetOperationAtOffset offset = This.GetOperationAtPointer (This.Pointer + offset)

            member This.CurrentInstruction = This.GetOperationAtPointer This.Pointer

            member This.IncrementPointer offset = 
                This.History <- This.Pointer :: This.History
                match offset with
                | x when Seq.contains (This.Pointer + offset) This.History -> 
                    Errors.InfiniteLoopException( "Program entered an infinite loop.", This.Accumulator) |> raise
                | x when This.IsOffsetOutOfRange x -> 
                    Errors.OutOfRangeException ("Attempted to increment pointer out of memory", This.Accumulator)|> raise
                | x -> This.Pointer <- This.Pointer + x

            member This.IncrementAccumumlator value = This.Accumulator <- This.Accumulator + value

            member This.Tick () = 
                match This.CurrentInstruction with
                | Types.NOP _ -> This.IncrementPointer 1
                | Types.JMP offset -> This.IncrementPointer offset
                | Types.ACC value  -> 
                    This.IncrementAccumumlator value
                    This.IncrementPointer 1

            member This.SetOperationAtPointer (operation: Types.Operation) (pointer: int) = 
                let array = Seq.toArray This.Instructions
                array.SetValue(operation, pointer)
                This.Instructions <- Seq.toList array
        end

module Parsers =
    let ParseInstruction instruction : Types.Operation = 
        let mutable out = 0
        match instruction with
        | x when Utilities.MatchInstruction(@"nop (?<val>[+|-]\d+)", x, &out) -> Types.NOP out
        | x when Utilities.MatchInstruction(@"acc (?<val>[+|-]\d+)", x, &out) -> Types.ACC out
        | x when Utilities.MatchInstruction(@"jmp (?<val>[+|-]\d+)", x, &out) -> Types.JMP out
        | _ ->  Errors.InvalidOperationException "Invalid Instruction" |> raise
    
    let BuildProgram instructions = 
        let instructionSet = Seq.map ParseInstruction instructions |> Seq.toList in
            Computer.Program(instructionSet)