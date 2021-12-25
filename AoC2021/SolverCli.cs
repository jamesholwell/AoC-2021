using System.Reflection;
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

    public void Solve(string day, string inputPath, string solverHint, bool isPartTwo) {
        if (!TryReadInput(day, inputPath, out var input)) return;

        // resolve solver
        ISolver? solver;
        try {
            solver = this.factory.Create(day, input, solverHint);
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
        
        var sw = new Stopwatch();
        sw.Start();

        var solution = isPartTwo switch {
            false => solver.SolvePartOne(),
            true => solver.SolvePartTwo()
        };
        
        sw.Stop();
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        if (sw.ElapsedMilliseconds > 0)
            this.console.WriteLine($"# Runtime {sw.ElapsedMilliseconds}ms");
        else
            this.console.WriteLine($"# Runtime {sw.ElapsedTicks} ticks");
        Console.ResetColor();

        this.console.WriteLine(string.Empty);
        this.console.WriteLine(solution);
    }

    public void Benchmark(string day, string inputPath, string solverHint, bool isPartTwo) {
        if (!TryReadInput(day, inputPath, out var input)) return;

        var solvers = this.factory.CreateAll(day, input);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        this.console.WriteLine($"# Found {solvers.Count} solvers");
        Console.ResetColor();

        this.console.WriteLine(string.Empty);
        this.console.WriteLine($"|{"Solver",-25}|{"Solution",-35}|{"Runtime",-15}|");
        this.console.WriteLine("|-------------------------|-----------------------------------|---------------|");

        foreach (var pair in solvers) {
            var solverName = pair.Key;
            var solver = pair.Value;
            
            var sw = new Stopwatch();
            var runs = 1;
            var solution = string.Empty;
            
            sw.Start();
            solution = isPartTwo switch {
                false => solver.SolvePartOne(),
                true => solver.SolvePartTwo()
            };
            sw.Stop();

            if (sw.ElapsedMilliseconds < 1000) {
                // do warmup for fast solvers
                while (sw.Elapsed.Seconds < 5 && ++runs < 1000) {
                    solution = isPartTwo switch {
                        false => solver.SolvePartOne(),
                        true => solver.SolvePartTwo()
                    };
                }
                runs = 0;
                sw.Reset();
                
                sw.Start();
                while (sw.Elapsed.Seconds < 10 && ++runs < 10000) {
                    solution = isPartTwo switch {
                        false => solver.SolvePartOne(),
                        true => solver.SolvePartTwo()
                    };
                }
                sw.Stop();
            }
            
            var averageMs = sw.ElapsedMilliseconds / (double)runs;
            var runtime = averageMs switch {
                > 5000 => $"{averageMs / 1000:N1}s",
                > 1000 => $"{averageMs / 1000:N2}s",
                > 50 => $"{averageMs:N0}ms",
                _ => $"{sw.ElapsedTicks / (double)runs} ticks"
            };

            this.console.WriteLine($"|{solverName,-25}|{solution.Trim(),-35}|{runtime,15}|");
        }
        
        this.console.WriteLine("|-------------------------|-----------------------------------|---------------|");
        this.console.WriteLine(string.Empty);
    }

    private bool TryReadInput(string day, string path, out string input) {
        if (string.IsNullOrWhiteSpace(path))
            path = $"./inputs/{filenameSanitizer.Replace(day, string.Empty)}.txt";

        if (!File.Exists(path)) {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            this.console.WriteLine($"Path not found: {path}");
            Console.ResetColor();

            input = string.Empty;
            return false;
        }

        input = File.ReadAllText(path);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        this.console.WriteLine($"# Using input {Path.GetFileName(path)} ({input.Length} characters)");
        Console.ResetColor();
        
        return true;
    }
}