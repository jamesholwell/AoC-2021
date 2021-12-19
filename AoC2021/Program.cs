using System.CommandLine;

using AoC2021;
using AoC2021.Core;

var dayArgument = new Argument<string>("day", "Which puzzle to solve e.g. day1");
var inputArgument = new Argument<string>("input", "Path to puzzle input");
var solverArgument = new Option<string>("--solver", "Puzzle solver to use e.g. csharp, fsharp or specify solver name");

var root = new RootCommand { dayArgument, inputArgument, solverArgument };
root.SetHandler(
    (IConsole console, string day, string inputPath, string solverHint) => {
        var factory =
            new SolverFactory()
                .AddAssembly<AoC2021.CSharp.Day0>("csharp")
                .AddAssembly<AoC2021.FSharp.Day0>("fsharp");

        var cli = new SolverCli(console, factory);
        cli.Execute(day, inputPath, solverHint);
    },
    dayArgument,
    inputArgument,
    solverArgument);

await root.InvokeAsync(args);