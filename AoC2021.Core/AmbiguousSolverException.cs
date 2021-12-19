namespace AoC2021.Core;

using System.Collections.ObjectModel;

public class AmbiguousSolverException : Exception {
    public AmbiguousSolverException(string[] candidates)
        : base("Multiple solvers found") {
        this.Candidates = new ReadOnlyCollection<string>(candidates);
    }

    public ReadOnlyCollection<string> Candidates { get; }
}