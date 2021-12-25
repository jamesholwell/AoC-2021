using AoC2021.Core;

using Xunit;

namespace AoC2021.CSharp;

public class Day0 : Solver {
    public Day0(string? input) : base(input) { }

    public override string SolvePartOne() {
        return "Day 0 C#: " + Input;
    }

    [Fact]
    public void SolvesExample() {
        const string? exampleInput = @"foo";

        var solver = new Day0(exampleInput);
        var actual = solver.SolvePartOne();

        Assert.Equal("Day 0 C#: foo", actual);
    }
}