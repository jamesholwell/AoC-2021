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
    
    let GetMagnitude = fun (h, d) -> h * d
            
    let Solve =
        Shared.Split >>
        Seq.fold UpdateVector (0,0) >>
        GetMagnitude

type Day2() =
    interface ISolver with
        member this.SolvePartOne(s: string) = Day2.Solve s |> string
        member this.SolvePartTwo(s: string) = ""
