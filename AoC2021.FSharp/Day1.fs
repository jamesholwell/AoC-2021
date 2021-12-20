namespace AoC2021.FSharp

open AoC2021.Core
open Xunit

module Day1 =
    let CountIncreases =
        Seq.pairwise
        >> Seq.fold (fun acc (l, r) -> if r > l then acc + 1 else acc) 0

    let Solve = Shared.SplitInt >> CountIncreases

    let exampleInput =
        @"199
200
208
210
200
207
240
269
260
263
"

    [<Fact>]
    let ``Solves Part One Example`` () =
        let actual = Solve exampleInput
        Assert.Equal(7, actual)

type Day1(input: string) =
    interface ISolver with
        member this.SolvePartOne() = Day1.Solve input
        member this.SolvePartTwo() = failwith "NotImplemented"
