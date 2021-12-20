namespace AoC2021.CSharp;

using System.Text;

using AoC2021.Core;

public class Day0 : ISolver {
    public string SolvePartOne(string input) {
        return "Day 0 C#: " + input;
    }

    public string SolvePartTwo(string input) {
        throw new NotImplementedException();
    }
}

[Solver("day0", "alternative")]
public class Day0Alternative : ISolver {
    public string SolvePartOne(string input) {
        var sb = new StringBuilder(32);
        sb.Append("Day 0 C# Alternative: ");
        sb.Append(input);
        return sb.ToString();
    }

    public string SolvePartTwo(string input) {
        throw new NotImplementedException();
    }
}