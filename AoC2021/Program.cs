using System.CommandLine;

using AoC2021;
using AoC2021.Core;

var factory =
    new SolverFactory()
        .AddAssembly<AoC2021.CSharp.Day0>("csharp")
        .AddAssembly<AoC2021.FSharp.Day0>("fsharp");

var dayArgument = new Argument<string>("day", "Puzzle to solve e.g. day1");
var inputArgument = new Option<string>("--input", "Path to puzzle input e.g. .\\inputs\\day1.txt");
var solverArgument = new Option<string>("--solver", "Solver to use e.g. csharp, fsharp or specify solver name");
var partArgument = new Option<bool>("--part2", "Solve part two of the puzzle");
var benchmarkArgument = new Option<bool>("--bench", "Benchmark solver performance");

var root = new RootCommand { dayArgument, inputArgument, solverArgument, partArgument, benchmarkArgument };
root.SetHandler(
    (IConsole console, string day, string inputPath, string solverHint, bool isPartTwo, bool isBenchmarking) => {
        var cli = new SolverCli(console, factory);
        
        if (isBenchmarking) 
            cli.Benchmark(day, inputPath, solverHint, isPartTwo);
        else
            cli.Solve(day, inputPath, solverHint, isPartTwo);
    },
    dayArgument,
    inputArgument,
    solverArgument,
    partArgument,
    benchmarkArgument);

await root.InvokeAsync(args);