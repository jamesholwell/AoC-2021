module Day21Tests

open AoC2021.Core
open AoC2021.FSharp
open AoC2021.FSharp.Day21
open Xunit

let exampleInput = @"
Player 1 starting position: 4
Player 2 starting position: 8
"

[<Fact>]
let ``Parser reads input as expected`` () =
    let player1Position, player2Position = Parse exampleInput
    Assert.Equal(4, player1Position)
    Assert.Equal(8, player2Position)
    
[<Fact>]
let ``Deterministic die rolls as expected`` () =
    let s = Day21.DeterministicDie 100
    Assert.Equal(1, Seq.item 0 s)
    Assert.Equal(100, Seq.item 99 s)
    Assert.Equal(1, Seq.item 100 s)
    Assert.Equal(50, Seq.item 149 s)
    
[<Fact>]
let ``Roller scores as expected`` () =
    let s = DeterministicDie 100 |> DieRoller 3
    Assert.Equal(1 + 2 + 3, Seq.item 0 s)
    Assert.Equal(4 + 5 + 6, Seq.item 1 s)
    
[<Fact>]
let ``Turn taker behaves as expected`` () =
    let rules = { BoardSize = 10; NumberOfRolls = 3; TargetScore = 1000 }
    let state = { Name = "Test"; Location = 4; Score = 0; Rolls = 0 }
    Assert.Equal({ Name = "Test"; Location = 7; Score = 7; Rolls = 3 }, TakeTurn rules state 3)
    Assert.Equal({ Name = "Test"; Location = 1; Score = 1; Rolls = 3 }, TakeTurn rules state 7)

[<Fact>]
let ``Solves Part One Example`` () =
    let solver = upcast new Day21() : ISolver
    let actual = solver.SolvePartOne exampleInput
    Assert.Equal("739785", actual)
    
[<Fact>]
let ``Solves Part Two Example`` () =
    let solver = upcast new Day21() : ISolver
    let actual = solver.SolvePartTwo exampleInput
    Assert.Equal("444356092776315", actual)
