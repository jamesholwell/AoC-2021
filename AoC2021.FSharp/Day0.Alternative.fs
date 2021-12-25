namespace AoC2021.FSharp

open AoC2021.Core
open Xunit

module Day0Alternative =
    let Solve = sprintf "Day 0 F#: %s"

    [<Fact>]
    let ``Solves Example`` () =
        let exampleInput = @"foo"

        let actual = Solve exampleInput
        Assert.Equal("Day 0 F#: foo", actual)

[<Solver("day0", "sprintf")>]
type Day0Alternative(input: string) =
    interface ISolver with
        member this.SolvePartOne() = Day0.Solve input
        member this.SolvePartTwo() = failwith "NotImplemented"
