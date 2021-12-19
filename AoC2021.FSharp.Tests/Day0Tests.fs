module Day0Tests

open AoC2021.FSharp
open Xunit

[<Fact>]
let ``Solves Example`` () =
    let exampleInput = @"foo"

    let actual = Day0.Solve exampleInput
    Assert.Equal("Day 0 F#: foo", actual)
