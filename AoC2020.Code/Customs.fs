namespace AoC2020.Code

module Customs =
    type Individual = 
        struct
            val AnsweredQuestions: char Set
            new(input: string) = {AnsweredQuestions = input.ToCharArray() |> Set.ofArray}

            member This.Count = This.AnsweredQuestions.Count
        end

    type UnionGroup = 
        struct 
            val AnsweredQuestions: char Set
            new(people : Individual list) = {
                AnsweredQuestions = people |> Seq.map (fun x -> x.AnsweredQuestions) |> Set.unionMany}
            new (input: string list) = UnionGroup(input |> Seq.map (fun x -> new Individual(x)) |> Seq.toList)

            member This.Count = This.AnsweredQuestions.Count
        end

    type IntersectionGroup = 
        struct 
            val AnsweredQuestions: char Set
            new(people : Individual list) = {
                AnsweredQuestions = people |> Seq.map (fun x -> x.AnsweredQuestions) |> Set.intersectMany}
            new (input: string list) = IntersectionGroup(input |> Seq.map (fun x -> new Individual(x)) |> Seq.toList)

            member This.Count = This.AnsweredQuestions.Count
        end