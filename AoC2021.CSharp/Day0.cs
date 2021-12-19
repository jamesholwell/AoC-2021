namespace AoC2021.CSharp;

using System.Text;

using AoC2021.Core;

public class Day0 : ISolver {
    public string Solve(string input) {
        return "Day 0 C#: " + input;
    }
}

[Solver("day0", "alternative")]
public class Day0Alternative : ISolver {
    public string Solve(string input) {
        var sb = new StringBuilder(32);
        sb.Append("Day 0 C# Alternative: ");
        sb.Append(input);
        return sb.ToString();
    }
}