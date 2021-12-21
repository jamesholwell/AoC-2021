namespace AoC2021.FSharp

open AoC2021.Core

module Day2 =
    let (|Prefix|_|) (p: string) (s: string) =
        if s.StartsWith p then Some (s.Substring(p.Length) |> int)
        else None
    
    let UpdateVector = fun (h, d) s ->
        match s with
        | Prefix "forward" x -> (h + x, d)
        | Prefix "down"    x -> (h, d + x)
        | Prefix "up"      x -> (h, d - x)
        | _                  -> (h, d)
        
    let GetMagnitude (h, d) = h * d |> string
    
    let UpdateAimedVector = fun (h, d, a) s ->
        match s with
        | Prefix "forward" x -> (h + x, d + x * a, a)
        | Prefix "down"    x -> (h, d, a + x)
        | Prefix "up"      x -> (h, d, a - x)
        | _                  -> (h, d, a)
        
    let GetMagnitude3 (h, d, _) = h * d |> string

type Day2() =
    interface ISolver with
        member this.SolvePartOne(s: string) = Shared.Split s |> Seq.fold Day2.UpdateVector (0,0) |> Day2.GetMagnitude
        member this.SolvePartTwo(s: string) = Shared.Split s |> Seq.fold Day2.UpdateAimedVector (0,0,0) |> Day2.GetMagnitude3
