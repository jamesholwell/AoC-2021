using System.CommandLine;

using AoC2021;
using AoC2021.Core;

var dayArgument = new Argument<string>("day", "Which puzzle to solve e.g. day1");
var inputArgument = new Option<string>("--input", "Path to puzzle input e.g. .\\inputs\\day1.txt");
var solverArgument = new Option<string>("--solver", "Puzzle solver to use e.g. csharp, fsharp or specify solver name");
var partArgument = new Option<bool>("--part2", "Use part two solver");

var root = new RootCommand { dayArgument, inputArgument, solverArgument, partArgument };
root.SetHandler(
    (IConsole console, string day, string inputPath, string solverHint, bool isPartTwo) => {
        var factory =
            new SolverFactory()
                .AddAssembly<AoC2021.CSharp.Day0>("csharp")
                .AddAssembly<AoC2021.FSharp.Day0>("fsharp");

        var cli = new SolverCli(console, factory);
        cli.Execute(day, inputPath, solverHint, isPartTwo);
    },
    dayArgument,
    inputArgument,
    solverArgument,
    partArgument);

await root.InvokeAsync(args);