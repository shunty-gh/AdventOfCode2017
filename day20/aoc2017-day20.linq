<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 20, parts 1 & 2
    int day = 20;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);
    var result = Solve(input);
    Console.WriteLine($"Closest particle in the long run: {result.Part1}");
    Console.WriteLine($"Number of particles after all collisions: {result.Part2}");
}
    
public class Triplet
{
    public int X;
    public int Y;
    public int Z;

    public int Magnitude => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
    public string Pos => $"{X}_{Y}_{Z}";
    public void Add(Triplet t)
    {
        X += t.X;
        Y += t.Y;
        Z += t.Z;
    }
}

public static string pattern = @"[pva]=<(?<dx>-?\d*),(?<dy>-?\d*),(?<dz>-?\d*)>";
public static Regex re = new Regex(pattern);
public Triplet ToTriplet(string source)
{
    var match = re.Match(source);
    return new Triplet
    {
        X = int.Parse(match.Groups["dx"].Value),
        Y = int.Parse(match.Groups["dy"].Value),
        Z = int.Parse(match.Groups["dz"].Value),
    };
}

public void Move((int index, Triplet p, Triplet v, Triplet a) particle)
{
    particle.v.Add(particle.a);
    particle.p.Add(particle.v);
}
    
public (int Part1, int Part2) Solve(IEnumerable<string> input)
{
    var particles = new List<(int Index, Triplet p, Triplet v, Triplet a)>();

    var index = 0;
    foreach (var line in input)
    {        
        var splits = line.Split(new [] { ", "}, StringSplitOptions.RemoveEmptyEntries);
        var p = ToTriplet(splits[0]);
        var v = ToTriplet(splits[1]);
        var a = ToTriplet(splits[2]);
        particles.Add((index, p, v, a));
        index++;
    }
    
    // Index of the item with the smallest absolute acceleration - part 1
    var closest = particles
        .OrderBy(ipva => ipva.a.Magnitude)
        .First();

    // Move each particle and after a while collisions should stop happening.
    // Dunno how, strictly, we can determine when that happens and life's too 
    // short too worry.
    // 10,000 moves should be plenty sufficient...
    var rounds = 10_000;
    while (rounds-- > 0)
    {
        var collisions = particles.GroupBy(pva => pva.p.Pos)
            .Where(grp => grp.Count() > 1)
            .ToList();

        if (collisions.Count > 0)
            Console.WriteLine($"{collisions.Count} group{(collisions.Count == 1 ? "" : "s" )} of collisions in round {10_000 - rounds}");
            
        var indexes = new HashSet<int>(collisions.SelectMany(coll => coll.Select(prt => prt.Index)));

        particles = particles
            .Where(pva => !indexes.Contains(pva.Index))
            .ToList();

        particles.ForEach(pva => Move(pva));
    }
    
    return (closest.Index, particles.Count);
}
    