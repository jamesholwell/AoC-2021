namespace AoC2021.CSharp.Tests;

using Xunit;

public class Day25Tests {
    private const string ExampleInput = @"
v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>
";
    
    [Fact]
    public void SolvesExample() {
        var solver = new Day25();
        var actual = solver.SolvePartOne(ExampleInput);

        Assert.Equal("58", actual);
    }
}