namespace AoC2020.Code

module Sledding = 
    type Vector = int * int

    type Map = 
        struct
            val Height: int
            val Width: int
            val Array: bool [,]
            new(lines: string list) = 
                let height = lines.Length
                let width = lines.Head.Length in 
                    { 
                        Height = height; 
                        Width = width; 
                        Array = lines 
                        |> Seq.map Seq.toArray 
                        |> Seq.toArray 
                        |> function M -> Array2D.init width height (function i -> (function j -> M.[j].[i]))
                        |> Array2D.map (function x -> x = '#')
                    }

            member this.HasTree(x,y) = this.Array.[x % this.Width, y]

            member this.CountTrees (ox, oy) (dx, dy) = 
                let height = this.Height
                let hasTree = this.HasTree
                let rec CountTrees count step = 
                    let newX, newY = ox + step*dx, oy + step*dy in 
                        match oy + step * dy >= height with
                        | true -> count
                        | false -> match hasTree(newX, newY) with
                            | true -> CountTrees (count + 1) (step + 1)
                            | false -> CountTrees count (step+1)
                    in CountTrees 0 1 
        end
        