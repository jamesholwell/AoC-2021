module Day1Tests

open AoC2021.Core
open AoC2021.FSharp
open Xunit

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
    let actual = (Day1.SplitCast >> Day1.CountIncreases) exampleInput
    Assert.Equal(7, actual)
    
[<Fact>]
let ``Solves Part Two Example`` () =
    let solver = upcast Day1() : ISolver
    let actual = solver.SolvePartTwo exampleInput
    Assert.Equal("5", actual)

