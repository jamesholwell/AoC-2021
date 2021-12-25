using System.Text;

using AoC2021.Core;

using Xunit;

namespace AoC2021.CSharp;

[Solver("day0", "stringbuilder")]
public class Day0Alternative : Solver {
    public Day0Alternative(string? input = null) : base(input) { }

    public override string SolvePartOne() {
        var sb = new StringBuilder(32);
        sb.Append("Day 0 C# Alternative: ");
        sb.Append(Input);
        return sb.ToString();
    }

    [Fact]
    public void AlternativeSolvesExample() {
        const string exampleInput = @"foo";

        var solver = new Day0Alternative(exampleInput);
        var actual = solver.SolvePartOne();

        Assert.Equal("Day 0 C# Alternative: foo", actual);
    }
}