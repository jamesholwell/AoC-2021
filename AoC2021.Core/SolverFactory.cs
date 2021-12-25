namespace AoC2021.Core;

using System.Reflection;

public class SolverFactory {
    private readonly Dictionary<string, Type> solvers;

    public SolverFactory() {
        this.solvers = new Dictionary<string, Type>();
    }

    public ISolver? Create(string day, string input, string? solver = null) {
        var prefix = $"{day.ToLowerInvariant()}-{solver}";
        var candidates = this.solvers.Keys.Where(k => k.StartsWith(prefix)).ToArray();

        return candidates.Length switch {
            1 => Activator.CreateInstance(this.solvers[candidates.Single()], input) as ISolver,
            0 => null,
            _ => throw new AmbiguousSolverException(candidates)
        };
    }

    public IDictionary<string, ISolver> CreateAll(string day, string input) =>
        this.solvers.Keys.Where(k => k.StartsWith($"{day.ToLowerInvariant()}-"))
            .ToDictionary(k => k, k => (ISolver) Activator.CreateInstance(this.solvers[k], input)!);

    public SolverFactory AddAssembly<T>(string prefix) {
        foreach (var type in typeof(T).Assembly.GetTypes().Where(t => typeof(ISolver).IsAssignableFrom(t))) {
            var solverAttribute = type.GetCustomAttribute<SolverAttribute>();
            this.solvers[solverAttribute?.Key ?? type.Name.ToLowerInvariant() + "-" + prefix] = type;
        }

        return this;
    }
}