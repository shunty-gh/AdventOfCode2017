<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 1, part 1 & 2
    // Another alternative using some convoluted Linq

    var fname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day01-part1.txt");
    var input = File.ReadAllText(fname, Encoding.UTF8);

    var result1 = Solve(input, 1);
    var result2 = Solve(input, input.Length / 2);

    Console.WriteLine($"Result, part 1: {result1}");
    Console.WriteLine($"Result, part 2: {result2}");
}

public int Solve(string input, int step)
{
    var sum = input.ToCharArray()
        .Select(c => (int)(c - '0'))
        .Zip((input + input.Substring(0, step))
            .ToCharArray()
            .Select(c => (int)(c - '0'))
            .Skip(step),
            (l1, l2) => l1 == l2 ? l1 : 0)
        .Sum(n => n);
    
    return sum;
}
