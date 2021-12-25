using System.Reflection;

namespace AoC2021.Core;

public class SolverFactory {
    private readonly Dictionary<string, Type> solvers;

    public SolverFactory() {
        solvers = new Dictionary<string, Type>();
    }

    public ISolver? Create(string day, string input, string? solver = null) {
        var prefix = $"{day.ToLowerInvariant()}-{solver}";
        var candidates = solvers.Keys.Where(k => k.StartsWith(prefix)).ToArray();

        return candidates.Length switch {
            1 => Activator.CreateInstance(solvers[candidates.Single()], input) as ISolver,
            0 => null,
            _ => throw new AmbiguousSolverException(candidates)
        };
    }

    public IDictionary<string, ISolver> CreateAll(string day, string input) {
        return solvers.Keys.Where(k => k.StartsWith($"{day.ToLowerInvariant()}-"))
            .ToDictionary(k => k, k => (ISolver) Activator.CreateInstance(solvers[k], input)!);
    }

    public SolverFactory AddAssembly<T>(string prefix) {
        foreach (var type in typeof(T).Assembly.GetTypes().Where(t => typeof(ISolver).IsAssignableFrom(t))) {
            var solverAttribute = type.GetCustomAttribute<SolverAttribute>();
            solvers[solverAttribute?.Key ?? type.Name.ToLowerInvariant() + "-" + prefix] = type;
        }

        return this;
    }
}