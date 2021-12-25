﻿namespace AoC2021.FSharp

open AoC2021.Core
open Xunit

module Day0 =
    let Solve s = "Day 0 F#: " + s

    [<Fact>]
    let ``Solves Example`` () =
        let exampleInput = @"foo"

        let actual = Solve exampleInput
        Assert.Equal("Day 0 F#: foo", actual)

type Day0(input: string) =
    interface ISolver with
        member this.SolvePartOne() = Day0.Solve input
        member this.SolvePartTwo() = failwith "NotImplemented"
