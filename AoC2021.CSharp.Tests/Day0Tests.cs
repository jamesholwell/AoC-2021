namespace AoC2021.CSharp.Tests;

using Xunit;

public class Day10Tests {
    [Fact]
    public void SolvesExample() {
        const string ExampleInput = @"foo";

        var solver = new Day0();
        var actual = solver.Solve(ExampleInput);

        Assert.Equal("Day 0 C#: foo", actual);
    }

    [Fact]
    public void AlternativeSolvesExample() {
        const string ExampleInput = @"foo";

        var solver = new Day0Alternative();
        var actual = solver.Solve(ExampleInput);

        Assert.Equal("Day 0 C# Alternative: foo", actual);
    }
}