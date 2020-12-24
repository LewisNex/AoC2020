namespace AoC2020.Code

open System.Text.RegularExpressions

module Luggage = 
    let AddPair(child, parent, count, children : Map<string, System.Tuple<string, int> list>, parents : Map<string, string list>) = 
        let newParents =
            match parents.ContainsKey child with
            | true -> 
                let current = parents.[child] in
                parents.Add (child, parent::current)
            | false -> parents.Add(child, [parent])
        let newChildren =
            match children.ContainsKey parent with
            | true -> 
                let current = children.[parent] in
                children.Add (parent, (child, count)::current)
                | false -> children.Add(parent, [(child, count)]) in
        newChildren, newParents

    
    let foldFunc (parent: string) (state: Map<string, System.Tuple<string, int> list> * Map<string, string list>) (child: string, count:int)  = 
        let childrenAcc, parentAcc = state in
            AddPair(child,parent,count,childrenAcc,parentAcc)

    let GetParentAndChildrenFromRule ((initialChildren: Map<string, System.Tuple<string, int> list>), (initialParents: Map<string, string list>)) (rule: string) = 
        let rMatch = Regex.Match(rule, @"(?<key>\b[\d\w\s]+) bags contain(?<vals>( \d+ \b[\d\w\s]+ bag[s]?[, |.])+)") in
        match rMatch.Success with
        | false -> initialChildren, initialParents
        | _ -> 
            let parent = rMatch.Groups.["key"].Value
            let values = rMatch.Groups.["vals"].Value
            let (finalChildren, finalParents) = 
                Regex.Matches(values, @"(?<count>\d+) (?<colour>\b[\d\w\s]+) bag[s]?[, |.]") 
                |> Seq.cast<Match> 
                |> Seq.map (fun x -> x.Groups.["colour"].Value, x.Groups.["count"].Value |> int)
                |> Seq.fold (foldFunc parent) ((initialChildren, initialParents)) 
                in
                finalChildren, finalParents

    let ParentAndChildrenFromRules (rules: string list) : Map<string, System.Tuple<string, int> list> * Map<string, string list> = 
        Seq.fold (GetParentAndChildrenFromRule) (Map.empty, Map.empty) rules

    let GetParentAndChildrenFromRuleEmpty rule = GetParentAndChildrenFromRule (Map.empty, Map.empty) rule 

    let GetAllParents (bag: string) (parents: Map<string, string list>) : string Set =
        let rec GetAllParents cBag = 
            match parents.ContainsKey cBag with
            | false -> Set.empty.Add cBag
            | true -> 
                let cParents = parents.[cBag] in
                    let others = Set.unionMany (cParents |> Seq.map GetAllParents)
                    others.Add cBag
        in (GetAllParents bag).Remove bag

    let GetCountOfChildren (bag: string) (children: Map<string, System.Tuple<string, int> list>) : int = 
        let rec GetChildrenCount cbag = 
            match children.ContainsKey cbag with
            | false -> 1
            | true ->
                let cChildren = children.[cbag] in
                    let others = Seq.map (fun x -> 
                        let c, count = x in
                         count * GetChildrenCount c) cChildren
                    in 1 + Seq.sum others
        in GetChildrenCount bag - 1