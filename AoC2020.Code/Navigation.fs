namespace AoC2020.Code

open System

module Navigation = 
    type Instruction = 
        NORTH of int 
        | EAST of int 
        | SOUTH of int 
        | WEST of int 
        | LEFT of int 
        | RIGHT of int 
        | FORWARD of int 

    type Coordinate = int * int

    type Quadrant =  NE | NW | SE | SW
    let GetQuadrant coord = 
        match coord with
        | x,y when x >= 0 && y>= 0 -> NE 
        | x,y when x <= 0 && y>= 0 -> NW 
        | x,y when x >= 0 && y<= 0 -> SE 
        | _ -> SW 

    let TurnByDegrees(current: int, change: int) = 
        match (current + change) % 360 with
        | x when x >= 0 -> x
        | x -> 360 + x

    type Ship = 
        struct
            val Coord: Coordinate
            val Waypoint: Coordinate
            new(c, w) = {Coord = c; Waypoint = w}
            new(cx, cy, wx, wy) = {Coord = (cx, cy); Waypoint = (wx, wy)}

            member This.ManhattanDistance = 
                let x,y = This.Coord
                Math.Abs x + Math.Abs y
        end

    let RotateWaypoint (w: Coordinate, angle: int) =
        let x, y = w
        match TurnByDegrees(0, angle) with
        | 0 -> w
        | 180 -> (-x, -y)
        | 90 -> 
            match GetQuadrant w with
            | NE -> (y, -x)
            | SE -> (y, -x)
            | NW -> (y, -x)
            | SW -> (y, -x)
        | 270 -> 
            match GetQuadrant w with
            | NE -> (-y, x)
            | SE -> (-y, x)
            | NW -> (-y, x)
            | SW -> (-y, x)
        | u -> Exception (sprintf "Invalid angle %d" angle) |> raise

    let rec Move (ship: Ship) (inst: Instruction) : Ship = 
        let (cx, cy) = ship.Coord
        let (wx, wy) = ship.Waypoint
        match inst with
        | NORTH x -> Ship(ship.Coord, (wx, wy+x))
        | SOUTH x -> Ship(ship.Coord, (wx, wy-x))
        | EAST x -> Ship(ship.Coord, (wx+x, wy))
        | WEST x -> Ship(ship.Coord, (wx-x, wy))
        | FORWARD x -> Ship ((cx+x*wx, cy+x*wy), ship.Waypoint)
        | LEFT x -> Ship(ship.Coord, RotateWaypoint(ship.Waypoint, -x))
        | RIGHT x -> Ship(ship.Coord, RotateWaypoint(ship.Waypoint, +x))

    let MoveMany (ship: Ship) (insts: Instruction seq) =
        Seq.fold (fun (ship: Ship) inst -> Move ship inst) ship insts


    let ParseInstruction instruction = 
        let mutable out = 0
        match instruction with
        | x when Utilities.MatchInstruction(@"N(?<val>\d+)", x, &out) -> NORTH out
        | x when Utilities.MatchInstruction(@"S(?<val>\d+)", x, &out) -> SOUTH out
        | x when Utilities.MatchInstruction(@"E(?<val>\d+)", x, &out) -> EAST out
        | x when Utilities.MatchInstruction(@"W(?<val>\d+)", x, &out) -> WEST out
        | x when Utilities.MatchInstruction(@"L(?<val>\d+)", x, &out) -> LEFT out
        | x when Utilities.MatchInstruction(@"R(?<val>\d+)", x, &out) -> RIGHT out
        | x when Utilities.MatchInstruction(@"F(?<val>\d+)", x, &out) -> FORWARD out
        | _ ->  Exception "Invalid Instruction" |> raise


