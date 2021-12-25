namespace AoC2021.CSharp;

using AoC2021.Core;

public class Day25 : ISolver {
    private const ushort Empty = 0;

    private const ushort EastFacing = 1;

    private const ushort SouthFacing = 2;

    private int w;
    
    private int h;

    private int l;

    public string SolvePartOne(string input) {
        var current = Parse(input);
        var next = new ushort[l];
        Array.Copy(current, next, l);
        
        var steps = 0;
        var shouldLoop = true;
        
        while (shouldLoop) {
            ++steps;
            shouldLoop = false;

            for (var y = 0; y < h; ++y) {
                var offset = y * w;

                for (var x = 0; x < w; ++x) {
                    if (current[offset + x] == EastFacing && current[offset + (x + 1) % w] == Empty) {
                        next[offset + x] = Empty;
                        next[offset + (x + 1) % w] = EastFacing;
                        shouldLoop = true;
                    }
                }
            }

            Array.Copy(next, current, l);
        
            for (var y = 0; y < h; ++y) {
                var offset = y * w;

                for (var x = 0; x < w; ++x) {
                    if (current[offset + x] == SouthFacing && current[(offset + x + w) % l] == Empty) {
                        next[offset + x] = Empty;
                        next[(offset + x + w) % l] = SouthFacing;
                        shouldLoop = true;
                    }
                }
            }
        
            Array.Copy(next, current, l);
        }

        return steps.ToString();
    }

    private ushort[] Parse(string input) {
        var lines = input
            .Replace("\r", string.Empty)
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .ToArray();
        
        w = lines[0].Length;
        h = lines.Length;
        l = w * h;
        
        var matrix = new ushort[h * w];
        for (var y = 0; y < h; ++y)
        for (var x = 0; x < w; ++x)
            matrix[x + y * w] = lines[y][x] switch {
                '>' => EastFacing,
                'v' => SouthFacing,
                _   => Empty
            };

        return matrix;
    }
    
    public string SolvePartTwo(string input) {
        throw new NotImplementedException();
    }
}