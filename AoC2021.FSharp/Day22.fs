namespace AoC2021.FSharp

open System
open AoC2021.Core

module Day22 =
    type State = On | Off
  
    type Range = {
        Min: Int64
        Max: Int64
    }
    
    type Cuboid = {
        X: Range
        Y: Range
        Z: Range
    }
            
    // range intersects
    let (@=) r1 r2 = r1.Min <= r2.Max && r1.Max >= r2.Min
    
    // cuboid intersects
    let (@==) (c1, _) (c2, _) = c1.X @= c2.X && c1.Y @= c2.Y && c1.Z @= c2.Z
    
    let (|Prefix|_|) (p: string) (s: string) =
        if s.StartsWith p then Some (s.Substring(p.Length))
        else None
              
    let ParseInstruction s =
        let ParseRange (s: string) =
            let parts = s.Substring(2).Split("..")
            {
                Min = int64 parts[0]
                Max = int64 parts[1]
            }
        
        let ParseCuboid (s: string) =
            let parts = s.Split (",", StringSplitOptions.RemoveEmptyEntries)
            {
                X = ParseRange parts[0]
                Y = ParseRange parts[1]
                Z = ParseRange parts[2]
            }
            
        match s with
        | Prefix "on "   x -> ParseCuboid x, On
        | Prefix "off "  x -> ParseCuboid x, Off
        | _                -> failwith "Oof!"
        
    let Parse s = Shared.Split s |> Array.map ParseInstruction
    
    let TrimInstructionsForPart1 =
        let ia = { Min = -50L ; Max = 50L }
        Seq.choose (fun (i, s) -> if ia @= i.X && ia @= i.Y && ia @= i.Z then Some (i, s) else None)

    let Decompose (target: Cuboid, state: State) (others : seq<Cuboid * State>) =
        // Splits the target cuboid into a disjoint union of non-intersecting cubes 
        let all = Seq.append (seq { target }) (Seq.map fst others) |> Seq.toArray
                
        let xs = all |> Array.collect (fun c -> [| c.X.Min; c.X.Max + 1L |]) |> Array.filter (fun x -> target.X.Min <= x && x <= target.X.Max + 1L) |> Array.sort |> Array.distinct |> Array.pairwise
        let ys = all |> Array.collect (fun c -> [| c.Y.Min; c.Y.Max + 1L |]) |> Array.filter (fun y -> target.Y.Min <= y && y <= target.Y.Max + 1L) |> Array.sort |> Array.distinct |> Array.pairwise
        let zs = all |> Array.collect (fun c -> [| c.Z.Min; c.Z.Max + 1L |]) |> Array.filter (fun z -> target.Z.Min <= z && z <= target.Z.Max + 1L) |> Array.sort |> Array.distinct |> Array.pairwise
        
        seq {
            for x1, x2 in xs do
            for y1, y2 in ys do
            for z1, z2 in zs ->
                { X = { Min = x1; Max = x2 - 1L }; Y = { Min = y1; Max = y2 - 1L }; Z = { Min = z1; Max = z2 - 1L } }, state
        } |> Seq.toArray
         
    let Triangularise (s: seq<Cuboid * State>) =
        // From { 1; 2; 3; ...} produces { 1, {}   ;   2, { 1 }   ;   3, { 2 ; 1 }
        let state  = Seq.head s, Seq.empty
        let folder = fun (h, t) (item: Cuboid * State) -> item, (Seq.append [h] t)
        s |> Seq.tail |> Seq.scan folder state |> Seq.cache
        
    let VolumeOf c =
        (1L + c.X.Max - c.X.Min) * (1L + c.Y.Max - c.Y.Min) * (1L + c.Z.Max - c.Z.Min)
        
    let EffectOf (cuboid, state) predecessorState =
        match (predecessorState, state) with
        | None, On -> VolumeOf cuboid
        | Some (_, Off), On -> VolumeOf cuboid
        | Some (_, On), Off -> -VolumeOf cuboid
        | _ -> 0L
    
    let DecomposeAndFindEffects target others =
        let otherArray = Seq.toArray others;
        let findEffect decomposedTarget =
            otherArray |> Array.tryFind (fun o -> o @== decomposedTarget) |> EffectOf decomposedTarget
        Decompose target otherArray |> Array.Parallel.map findEffect |> Seq.sum
  
    let Execute cuboidStates =
        let triangle = Triangularise cuboidStates
        triangle |> Seq.map (fun (target, others) -> DecomposeAndFindEffects target others) |> Seq.sum

type Day22() =
    interface ISolver with
        member this.SolvePartOne(s: string) =
            Day22.Parse s |> Day22.TrimInstructionsForPart1 |> Day22.Execute |> string
            
        member this.SolvePartTwo(s: string) =
            Day22.Parse s |> Day22.Execute |> string
