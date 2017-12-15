<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 13 - Dumb approach, version 2
    
    /* **
     * This is a slightly leass naïve way of doing this puzzle but reduces the time taken 
     * taken to solve significantly by only moving the current Scanner/Layer rather than
     * all of them. Still not particularly smart though.
     * The proper solution is in aoc2017-day13.linq
     */
     
    int day = 13;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);

    var result = Solve(input);
    Console.WriteLine($"Severity with 0 delay (part 1): {result.Item1}");
    Console.WriteLine($"Delay required not to get caught (part 2): {result.Item2}");
}

public class Layer
{
    public int Depth;
    public int Range;
    public int ScannerPos;
    private int Direction;

    public void Move(int times, bool fromOrigin)
    {
        if (fromOrigin)
            ScannerPos = 0;
            
        var realtimes = times % (2 * (Range - 1));
        for (var index = 0; index < realtimes; index++)
        {
            if (ScannerPos == 0)
            {
                Direction = 1;
            }
            else if (ScannerPos == Range - 1)
            {
                Direction = -1;
            }
            ScannerPos += Direction;
        }
    }
}

public int Travel(IList<Layer> layers, int delay, bool breakIfCaught)
{
    int severity = 0;
    int depth = -1;
    int maxdepth = layers.Max(l => l.Depth);
    
    while (depth <= maxdepth)
    {
        depth++;
        var thislayer = layers.FirstOrDefault(l => l.Depth == depth);

        if (thislayer == null)
            continue;
        
        thislayer.Move(delay + depth, true);
        if (thislayer.ScannerPos == 0)
        {
            severity += (thislayer.Depth * thislayer.Range);
            if (breakIfCaught)
            {
                severity = int.MaxValue;
                break;   
            }
        }
    }
    
    return severity;
}

public Tuple<int, int> Solve(IEnumerable<string> input)
{
    var layers = input.Select(s =>
    {
        var splits = s.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
        return new Layer
        {
            Depth = int.Parse(splits[0]),
            Range = int.Parse(splits[1]),
        };
    })
    .ToList();

    // Part 1, 0 delay
    var part1 = Travel(layers, 0, false);

    int delay = 0;
    while (true)
    {
        var severity = Travel(layers, delay, true);
        if (severity == 0)
            break;
        delay++;
    }
    
    return new Tuple<int, int>(part1, delay);
}
