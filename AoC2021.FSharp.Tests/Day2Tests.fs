module Day2Tests

open AoC2021.Core
open AoC2021.FSharp
open Xunit

let exampleInput = @"
forward 5
down 5
forward 8
up 3
down 8
forward 2
"

[<Fact>]
let ``Solves Part One Example`` () =
    let solver = upcast new Day2() : ISolver
    let actual = solver.SolvePartOne exampleInput
    Assert.Equal("150", actual)

[<Fact>]
let ``Solves Part Two Example`` () =
    let solver = upcast new Day2() : ISolver
    let actual = solver.SolvePartTwo exampleInput
    Assert.Equal("900", actual)
    
