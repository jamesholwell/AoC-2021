namespace AoC2021.FSharp

open System
open AoC2021.Core

module Day1 =
    let CountIncreases =
        Seq.pairwise >>
        Seq.fold (fun acc (l, r) -> if r > l then acc + 1 else acc) 0
        
    let Solve (s: string) =
        s.Split ("\n", StringSplitOptions.RemoveEmptyEntries) 
        |> Seq.map int
        |> CountIncreases

type Day1() =
    interface ISolver with
        member this.Solve(s: string) = (Day1.Solve s).ToString()
