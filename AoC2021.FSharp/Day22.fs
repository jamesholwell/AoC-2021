namespace AoC2021.FSharp

open System
open AoC2021.Core

module Day22 =
    type CubeState = On | Off
    
    type Position = 
        int * int * int
    
    type Cube =
        Position * CubeState
    
    type Range = {
        Min: int
        Max: int
    }
    
    type Cuboid = {
        X: Range
        Y: Range
        Z: Range
    }
    
    type Instruction =
        | On of Cuboid
        | Off of Cuboid
    
    let (|Prefix|_|) (p: string) (s: string) =
        if s.StartsWith p then Some (s.Substring(p.Length))
        else None
        
    let ParseRange (s: string) =
        let parts = s.Substring(2).Split("..")
        {
            Min = int parts[0]
            Max = int parts[1]
        }
    
    let ParseCuboid (s: string) =
        let parts = s.Split (",", StringSplitOptions.RemoveEmptyEntries)
        {
            X = ParseRange parts[0]
            Y = ParseRange parts[1]
            Z = ParseRange parts[2]
        }
       
    let ParseInstruction s =
        match s with
        | Prefix "on "   x -> On (ParseCuboid x)
        | Prefix "off "  x -> Off (ParseCuboid x)
        | _                -> failwith "Oof!"
        
    let rec Parse s =
        Shared.Split s |> Array.map ParseInstruction
        
    let CreateReactor x1 x2 y1 y2 z1 z2 = seq {
        for x in x1 .. x2 do
            for y in y1 .. y2 do
                for z in z1 .. z2 ->
                    (x, y, z), CubeState.Off
    }
    
    let Outside x r = x < r.Min || r.Max < x
     
    let (|Inside|_|) (p: Cuboid) ((x, y, z), s) =
        if Outside x p.X || Outside y p.Y || Outside z p.Z then None
        else Some ((x, y, z), s)
            
    let Execute reactor instructions =
        let Execute cuboid state = function 
            | Inside cuboid ((x, y, z), _) -> (x, y, z), state
            | cube -> cube
                
        let folder = fun acc instruction ->
            match instruction with
            | On  cuboid -> Seq.map (Execute cuboid CubeState.On) acc
            | Off cuboid -> Seq.map (Execute cuboid CubeState.Off) acc
            
        Seq.fold folder reactor instructions
    
    let CountOnCubes =
        Seq.fold (fun acc ((x, y, z), s) -> match s with | CubeState.On -> 1 + acc | _ -> acc) 0 
        
type Day22() =
    interface ISolver with
        member this.SolvePartOne(s: string) =
            let reactor = Day22.CreateReactor -50 50 -50 50 -50 50 
            Day22.Parse s |> Day22.Execute reactor |> Day22.CountOnCubes |> string
            
        member this.SolvePartTwo(s: string) =
            let instructions = Day22.Parse s
            let reactor = Day22.CreateReactor -50 50 -50 50 -50 50 
            instructions |> Day22.Execute reactor |> Day22.CountOnCubes |> string
