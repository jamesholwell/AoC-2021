namespace AoC2021.Core;

public abstract class Solver : ISolver {
    protected Solver(string? input = null) {
        Input = input ?? string.Empty;
    }

    protected string Input { get; }

    public virtual string SolvePartOne() {
        throw new NotImplementedException();
    }

    public virtual string SolvePartTwo() {
        throw new NotImplementedException();
    }
}