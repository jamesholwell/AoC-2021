namespace AoC2021.Core;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class SolverAttribute : Attribute {
    public SolverAttribute(string day, string name) {
        this.Key = $"{day.ToLowerInvariant()}-{name.ToLowerInvariant()}";
    }

    public string Key { get; }
}