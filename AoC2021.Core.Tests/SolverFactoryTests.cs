namespace AoC2021.Core.Tests;

using AoC2021.CSharp;

using Xunit;

using Day0 = AoC2021.FSharp.Day0;

public class SolverFactoryTests {
    private readonly SolverFactory factory;

    public SolverFactoryTests() {
        this.factory = new SolverFactory().AddAssembly<CSharp.Day0>("csharp").AddAssembly<Day0>("fsharp");
    }

    [Fact]
    public void CanResolveDayOneCsharp() {
        var solver = this.factory.Create("Day0", "csharp");
        Assert.NotNull(solver);
        Assert.Equal(typeof(CSharp.Day0).FullName, solver!.GetType().FullName);
    }

    [Fact]
    public void CanResolveDayOneCsharpAlternative() {
        var solver = this.factory.Create("Day0", "alternative");
        Assert.NotNull(solver);
        Assert.Equal(typeof(Day0Alternative).FullName, solver!.GetType().FullName);
    }

    [Fact]
    public void CanResolveDayOneFsharp() {
        var solver = this.factory.Create("Day0", "fsharp");
        Assert.NotNull(solver);
        Assert.Equal(typeof(Day0).FullName, solver!.GetType().FullName);
    }
}