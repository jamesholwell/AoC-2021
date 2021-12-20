namespace AoC2021.Core;

public interface ISolver : ISolvePartOne, ISolvePartTwo { }

public interface ISolvePartOne {
    string SolvePartOne(string input);
}

public interface ISolvePartTwo {
    string SolvePartTwo(string input);
}