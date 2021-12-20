namespace AoC2021.FSharp

open AoC2021.Core

module Day0 =
    let Solve s = "Day 0 F#: " + s

type Day0() =
    interface ISolver with
        member this.SolvePartOne(s: string) = Day0.Solve s
        member this.SolvePartTwo(s: string) = failwith "NotImplemented"
