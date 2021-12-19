namespace AoC2021.Core;

using System.Reflection;

public class SolverFactory {
    private readonly Dictionary<string, Type> solvers;

    public SolverFactory(params Assembly[] assemblies) {
        this.solvers = new Dictionary<string, Type>();
    }

    public ISolver? Create(string day, string? solver = null) {
        var prefix = $"{day.ToLowerInvariant()}-{solver}";
        var candidates = this.solvers.Keys.Where(k => k.StartsWith(prefix)).ToArray();

        switch (candidates.Length) {
            case 0: return null;
            case 1: return Activator.CreateInstance(this.solvers[candidates.Single()]) as ISolver;
            default:
                throw new AmbiguousSolverException(candidates);
        }
    }

    public SolverFactory AddAssembly<T>(string prefix) {
        foreach (var type in typeof(T).Assembly.GetTypes().Where(t => typeof(ISolver).IsAssignableFrom(t))) {
            var solverAttribute = type.GetCustomAttribute<SolverAttribute>();
            this.solvers[solverAttribute?.Key ?? type.Name.ToLowerInvariant() + "-" + prefix] = type;
        }

        return this;
    }
}