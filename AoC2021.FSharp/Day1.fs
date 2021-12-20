namespace AoC2021.FSharp

open System
open AoC2021.Core

module Day1 =
    let CountIncreases =
        Seq.pairwise >>
        Seq.fold (fun acc (l, r) -> if r > l then acc + 1 else acc) 0
    
    let CountIncreasingWindows =
        Seq.windowed 3 >>
        Seq.map Seq.sum >>
        CountIncreases
    
    let SplitCast (s: string) =
        s.Split ("\n", StringSplitOptions.RemoveEmptyEntries) 
        |> Seq.map int

type Day1() =
    interface ISolver with
        member this.SolvePartOne(s: string) = s |> Day1.SplitCast |> Day1.CountIncreases |> string
        member this.SolvePartTwo(s: string) = s |> Day1.SplitCast |> Day1.CountIncreasingWindows |> string
