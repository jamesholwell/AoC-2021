using System.Text.RegularExpressions;

namespace AoC2021;

using System.CommandLine;
using System.Diagnostics;

using AoC2021.Core;

// TODO: replace colours with System.CommandLine.Rendering
internal class SolverCli {
    private readonly IConsole console;

    private readonly SolverFactory factory;

    private readonly Regex filenameSanitizer = new Regex("[^a-z0-9]", RegexOptions.IgnoreCase);

    public SolverCli(IConsole console, SolverFactory factory) {
        this.console = console;
        this.factory = factory;
    }

    public void Execute(string day, string inputPath, string solverHint, int part) {
        // resolve solver
        ISolver? solver;
        try {
            solver = this.factory.Create(day, solverHint);
        }
        catch (AmbiguousSolverException e) {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            this.console.WriteLine(e.Message);
            foreach (var candidate in e.Candidates) {
                this.console.Write("  ");
                this.console.WriteLine(candidate);
            }
            Console.ResetColor();
            
            return;
        }

        if (solver == null) {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            this.console.WriteLine("No solvers found");
            Console.ResetColor();
            
            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        this.console.WriteLine($"# Using solver {solver.GetType().FullName}");
        Console.ResetColor();

        if (string.IsNullOrWhiteSpace(inputPath))
            inputPath = $"./inputs/{filenameSanitizer.Replace(day, string.Empty)}.txt";
        
        if (!File.Exists(inputPath)) {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            this.console.WriteLine("Path not found");
            Console.ResetColor();

            return;
        }
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        this.console.WriteLine($"# Using input {Path.GetFileName(inputPath)}");
        Console.ResetColor();

        var input = File.ReadAllText(inputPath);

        var sw = new Stopwatch();
        sw.Start();

        var solution = part switch {
            1 => solver.SolvePartOne(input),
            2 => solver.SolvePartTwo(input),
            _ => throw new ArgumentOutOfRangeException(nameof(part), "Only parts 1 and 2 are accepted")
        };
        
        sw.Stop();
        
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        this.console.WriteLine($"# Runtime {sw.ElapsedMilliseconds}ms");
        Console.ResetColor();

        this.console.WriteLine(string.Empty);
        this.console.WriteLine(solution);
    }
}