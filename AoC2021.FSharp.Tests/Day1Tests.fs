module Day1Tests

open AoC2021.Core
open AoC2021.FSharp
open Xunit

[<Fact>]
let ``Solves Example`` () =
    let exampleInput = @"199
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
    let solver = upcast Day1() : ISolver
    let actual = solver.SolvePartOne exampleInput
    Assert.Equal("7", actual)

