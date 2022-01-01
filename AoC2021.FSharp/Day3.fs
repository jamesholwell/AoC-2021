namespace AoC2021.FSharp

open AoC2021.Core
open Xunit

module Day3 =
    let Solve input =
        let splitInput = Shared.Split input
        
        let onBitIncidences =
            Array.fold
                (fun acc (item: string) -> Array.mapi (fun i x -> if item[i] = '1' then x + 1 else x) acc)
                (Array.zeroCreate splitInput[0].Length)
                splitInput
           
        let bin2dec = Array.reduce (fun a b -> a * 2 + b) 
           
        let threshold = splitInput.Length / 2
        let gamma   = onBitIncidences |> Array.map (fun i -> if i > threshold then 1 else 0) |> bin2dec 
        let epsilon = onBitIncidences |> Array.map (fun i -> if i > threshold then 0 else 1) |> bin2dec 
        gamma * epsilon

    let exampleInput = @"
00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010
"

    [<Fact>]
    let ``Solves Part One Example`` () =
        let actual = Solve exampleInput
        Assert.Equal(198, actual)
    
        
type Day3(input: string) =
    interface ISolver with
        member this.SolvePartOne() = Day3.Solve input
        member this.SolvePartTwo() = failwith "NotImplemented"
