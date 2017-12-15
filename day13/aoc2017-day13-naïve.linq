<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 13 - Dumb approach
    
    /* **
     * This is a REALLY naïve way of doing this puzzle and just solves it by brute force
     * without any consideration for the simple maths that are actually involved.
     * The better solution is in aoc2017-day13.linq
     */
     
    int day = 13;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);

//    var input = new List<string> {
//        "0: 3",
//        "1: 2",
//        "4: 4",
//        "6: 4",
//    };
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

    public void Reset() 
    {
        ScannerPos = 0;
        Direction = 1;
    }

    public void Move(int times = 1)
    {
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
    
    public bool ScannerAtTop => ScannerPos == 0;
    public int Severity => Depth * Range;
}

public int Travel(IList<Layer> layers, int delay, bool breakIfCaught)
{
    int severity = 0;
    int depth = 0;
    int maxdepth = layers.Max(l => l.Depth);
    
    foreach (var l in layers)
    {
        l.Reset();   
        l.Move(delay);
    }
    //layers.Dump($"After reset (with delay {delay})");

    while (depth <= maxdepth)
    {
        var thislayer = layers.FirstOrDefault(l => l.Depth == depth);
        if (thislayer != null && thislayer.ScannerAtTop)
        {
            severity += thislayer.Severity;
            if (breakIfCaught)
            {
                severity = int.MaxValue;
                break;   
            }
        }
        foreach (var l in layers)
        {
            l.Move();
        }
        //layers.Dump($"{depth}");
        depth++;
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
    //.Take(5) // For testing
    .ToList();
    //layers.Dump();

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
