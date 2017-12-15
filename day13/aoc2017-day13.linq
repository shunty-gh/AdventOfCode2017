<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 13 - The compact (sensible) version
    int day = 13;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);
    var result = Solve(input);
    Console.WriteLine($"Severity with 0 delay (part 1): {result.Item1}");
    Console.WriteLine($"Delay required not to get caught (part 2): {result.Item2}");
}

public Tuple<int, int> Solve(IEnumerable<string> input)
{
    var layers = input.Select(s =>
    {
        var splits = s.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
        return new 
        {
            Depth = int.Parse(splits[0]),
            Range = int.Parse(splits[1]),
        };
    })
    .ToList();
    
    var part1 = layers.Where(l => l.Depth % (2 * (l.Range - 1)) == 0)
        .Sum(l => l.Depth * l.Range);

    int delay = 0;
    var caught = true;
    while (caught)
    {
        delay++;
        caught = layers.Any(l => (delay + l.Depth) % (2 * (l.Range - 1)) == 0);
    }
    return new Tuple<int, int>(part1, delay);
}