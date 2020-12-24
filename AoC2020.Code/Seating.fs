namespace AoC2020.Code

module Seating =
    type Tile = EmptySeat | OccupiedSeat | Floor | OutOfBounds

    let IsTileOccupied state = 
        match state with
        | OccupiedSeat -> true
        | _ -> false

    type Coordinate = int * int

    let GetAdjacentCoords (x,y) : Coordinate seq = 
        Seq.fold (fun acc x -> x::acc) [] [
            (x+1,y+1);
            (x+1,y);
            (x+1,y-1);
            (x,y-1);
            (x-1,y-1);
            (x-1,y);
            (x-1,y+1);
            (x,y+1)
        ] |> Seq.ofList

        
    let GetValueFromMap coord map = 
        match Map.containsKey coord map with
        | false -> OutOfBounds
        | _ -> map.[coord]

    let GetDirectionalSeats (x,y) (map: Map<Coordinate, Tile>) = 
        let IsDirectionOccupied (dx, dy) =
            let rec IsDirectionOccupied2 (cx, cy) =
                match GetValueFromMap (cx, cy) map with
                | OutOfBounds -> None
                | Floor -> IsDirectionOccupied2 (cx+dx, cy+dy)
                | x -> Some (cx,cy)
            in IsDirectionOccupied2 (x+dx,y+dy)
        in [
            IsDirectionOccupied(1,1);
            IsDirectionOccupied(1,0);
            IsDirectionOccupied(1,-1);
            IsDirectionOccupied(0,-1);
            IsDirectionOccupied(-1,-1);
            IsDirectionOccupied(-1,0);
            IsDirectionOccupied(-1,1);
            IsDirectionOccupied(0,1);
        ] 
        |> Seq.filter (fun x -> x.IsSome) 
        |> Seq.map (fun x -> x.Value)
        
    let ParseIntoMap (lines: string seq) = 
        let mutable newMap : Map<Coordinate, Tile> = Map.empty
        let mutable x = 0
        let mutable y = 0
        Seq.iter (fun line -> 
            let chars = Seq.cast<char> line
            Seq.iter (fun c ->
                match c with
                | 'L' -> newMap <- Map.add (x, y) EmptySeat newMap
                | '#' -> newMap <- Map.add (x, y) OccupiedSeat newMap
                | '.' -> newMap <- Map.add (x, y) Floor newMap
                | _ -> ()
                x <- x + 1
            ) chars
            x <- 0
            y <- y + 1
        ) lines
        newMap

    type Chair = 
        struct 
            val IsOccupied: bool
            val VisibleChairs: Coordinate seq
            val HasUpdated: bool
            new(coord, map: Map<Coordinate, Tile>) = 
                {HasUpdated = true; 
                IsOccupied = GetValueFromMap coord map |> IsTileOccupied; 
                VisibleChairs = GetDirectionalSeats coord map} 
            new(others, isOccupied) = {
                HasUpdated = true; 
                IsOccupied = isOccupied;
                VisibleChairs = others}
            new(others, isOccupied, hasUpdated) = {
                HasUpdated = hasUpdated; 
                IsOccupied = isOccupied;
                VisibleChairs = others}

            member This.Clone() = Chair(This.VisibleChairs, This.IsOccupied, This.HasUpdated)

            member This.Tick(others : Map<Coordinate, Chair>) : Chair = 
                let count = 
                    This.VisibleChairs 
                    |> Seq.filter (fun x -> others.[x].IsOccupied)
                    |> Seq.length in 
                match count with
                | x when x >= 5 -> 
                    match This.IsOccupied with
                    | false -> Chair(This.VisibleChairs, false, false)
                    | true ->  Chair(This.VisibleChairs, false, true)
                | x when x = 0 ->
                    match This.IsOccupied with
                    | false ->  Chair(This.VisibleChairs, true, true)
                    | true ->  Chair(This.VisibleChairs, true, false)
                | _ -> Chair(This.VisibleChairs, This.IsOccupied, false)
        end
    type Room = 
        struct 
            val mutable Chairs: Map<Coordinate, Chair>
            new(chairs) = {Chairs = chairs}

            member This.Tick() = 
                let cloned = This.Chairs
                This.Chairs <- Map.map (fun coord (chair: Chair) -> chair.Tick(cloned)) This.Chairs
                Map.filter (fun coord (x: Chair) -> x.HasUpdated) This.Chairs |> Seq.length >= 1

            member This.OccupiedCount() = 
                Map.filter (fun coord (x: Chair) -> x.IsOccupied) This.Chairs
                |> Seq.length
        end

    
    
    let ParseMapIntoRoom (map: Map<Coordinate, Tile>) = 
        let mutable chairs = Map.empty
        Map.iter (fun k v -> 
            match v with
            | OutOfBounds -> ()
            | Floor -> ()
            | x -> chairs <- Map.add k (Chair(k, map)) chairs
        ) map
        Room(chairs)

    type Layout =
        struct
            val mutable Map: Map<Coordinate, Tile>
            val mutable HasChanged: bool

            new(lines) = {Map = ParseIntoMap lines; HasChanged = true} 

            member This.IsOccupied coord = 
                match Map.containsKey coord This.Map with
                | false -> false
                | _ -> IsTileOccupied This.Map.[coord]

            member This.CloneMap() =
                let mutable newMap = Map.empty
                Map.iter (fun k v -> newMap <- Map.add k v newMap) This.Map
                newMap

            member This.Tick() = 
                let mutable cloned = This.CloneMap()
                let real = This.Map
                let mutable hasChanged = false

                let isTileOccupied coord = 
                    GetValueFromMap coord real = OccupiedSeat

                Map.iter (fun coord state -> 
                    let neighbours =
                        GetAdjacentCoords coord 
                        |> Seq.filter (fun x -> isTileOccupied x)
                        |> Seq.length
                    match state with
                    | EmptySeat when neighbours = 0 -> 
                        cloned <- Map.add coord OccupiedSeat cloned
                        hasChanged <- true
                    | OccupiedSeat when neighbours >= 4 ->
                        cloned <- Map.add coord EmptySeat cloned
                        hasChanged <- true
                    | _ -> ()
                ) This.Map
                This.Map <- cloned
                This.HasChanged <- hasChanged

            member This.OccupiedCount =
                (Map.filter (fun k v -> v = OccupiedSeat) This.Map).Count
        end
