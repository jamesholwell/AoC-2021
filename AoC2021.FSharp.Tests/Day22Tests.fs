module Day22Tests

open AoC2021.Core
open AoC2021.FSharp
open AoC2021.FSharp.Day22
open Xunit

let exampleInput = @"
on x=-20..26,y=-36..17,z=-47..7
on x=-20..33,y=-21..23,z=-26..28
on x=-22..28,y=-29..23,z=-38..16
on x=-46..7,y=-6..46,z=-50..-1
on x=-49..1,y=-3..46,z=-24..28
on x=2..47,y=-22..22,z=-23..27
on x=-27..23,y=-28..26,z=-21..29
on x=-39..5,y=-6..47,z=-3..44
on x=-30..21,y=-8..43,z=-13..34
on x=-22..26,y=-27..20,z=-29..19
off x=-48..-32,y=26..41,z=-47..-37
on x=-12..35,y=6..50,z=-50..-2
off x=-48..-32,y=-32..-16,z=-15..-5
on x=-18..26,y=-33..15,z=-7..46
off x=-40..-22,y=-38..-28,z=23..41
on x=-16..35,y=-41..10,z=-47..6
off x=-32..-23,y=11..30,z=-14..3
on x=-49..-5,y=-3..45,z=-29..18
off x=18..30,y=-20..-8,z=-3..13
on x=-41..9,y=-7..43,z=-33..15
on x=-54112..-39298,y=-85059..-49293,z=-27449..7877
on x=967..23432,y=45373..81175,z=27513..53682
"

[<Fact>]
let ``Parser reads input as expected`` () =
    let parserTestCases = @"
on x=10..12,y=10..12,z=10..12
off x=9..11,y=9..11,z=9..11
on x=1..2,y=3..4,z=5..6
"
    let steps = Parse parserTestCases
    
    let expected0 = On { X = { Min = 10; Max = 12 }; Y = { Min = 10; Max = 12 }; Z = { Min = 10; Max = 12 } }
    Assert.Equal(expected0, steps[0])
    
    let expected1 = Off { X = { Min = 9; Max = 11 }; Y = { Min = 9; Max = 11 }; Z = { Min = 9; Max = 11 } }
    Assert.Equal(expected1, steps[1])
    
    let expected2 = On { X = { Min = 1; Max = 2 }; Y = { Min = 3; Max = 4 }; Z = { Min = 5; Max = 6 } }
    Assert.Equal(expected2, steps[2])
        
[<Fact>]
let ``Solves Part One Example`` () =
    let solver = upcast new Day22() : ISolver
    let actual = solver.SolvePartOne exampleInput
    Assert.Equal("590784", actual)

