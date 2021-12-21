namespace AoC2021.FSharp

open AoC2021.Core
open Xunit

module Day2 =
    let (|Prefix|_|) (p: string) (s: string) =
        if s.StartsWith p then
            Some(s.Substring(p.Length) |> int)
        else
            None

    let UpdateVector =
        fun (h, d) s ->
            match s with
            | Prefix "forward" x -> (h + x, d)
            | Prefix "down"    x -> (h, d + x)
            | Prefix "up"      x -> (h, d - x)
            | _                  -> (h, d)

    let GetMagnitude = fun (h, d) -> h * d

    let Solve =
        Shared.Split
        >> Seq.fold UpdateVector (0, 0)
        >> GetMagnitude

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
        let actual = Solve exampleInput
        Assert.Equal(150, actual)

type Day2(input: string) =
    interface ISolver with
        member this.SolvePartOne() = Day2.Solve input
        member this.SolvePartTwo() = failwith "NotImplemented"
