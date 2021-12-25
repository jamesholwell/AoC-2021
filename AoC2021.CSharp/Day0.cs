using AoC2021.Core;

using Xunit;

namespace AoC2021.CSharp;

public class Day0 : Solver {
    public Day0(string? input = null) : base(input) { }

    public override long SolvePartOne() {
        return Input.ToCharArray().Aggregate(1L, (current, c) => current * c);
    }

    [Fact]
    public void SolvesExample() {
        const string? exampleInput = @"foo";

        var solver = new Day0(exampleInput);
        var actual = solver.SolvePartOne();

        Assert.Equal(1256742, actual);
    }
}