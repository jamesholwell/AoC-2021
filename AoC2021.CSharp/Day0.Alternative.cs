using System.Text;

using AoC2021.Core;

using Xunit;

namespace AoC2021.CSharp;

[Solver("day0", "for-loop")]
public class Day0Alternative : Solver {
    public Day0Alternative(string? input = null) : base(input) { }

    public override long SolvePartOne() {
        long hash = 1;
        for (var i = 0; i < Input.Length; ++i) hash *= Input[i];
        
        return hash;
    }

    [Fact]
    public void AlternativeSolvesExample() {
        const string exampleInput = @"foo";

        var solver = new Day0Alternative(exampleInput);
        var actual = solver.SolvePartOne();

        Assert.Equal(1256742, actual);
    }
}