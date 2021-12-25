using System.Collections.ObjectModel;

namespace AoC2021.Core;

public class AmbiguousSolverException : Exception {
    public AmbiguousSolverException(string[] candidates)
        : base("Multiple solvers found") {
        Candidates = new ReadOnlyCollection<string>(candidates);
    }

    public ReadOnlyCollection<string> Candidates { get; }
}