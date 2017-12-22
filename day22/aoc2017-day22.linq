<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
    // Advent of code 2017
    // Day 22
    int day = 22;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);
    //    var input = new List<string> {  // test input => part 1: 5587; part 2: 2511944
    //        "..#",
    //        "#..",
    //        "...",
    //    };
    var result1 = Solve(input, 10_000, 1);
    Console.WriteLine($"Result, part 1: {result1}");
    var result2 = Solve(input, 10_000_000, 2);
    Console.WriteLine($"Result, part 2: {result2}");
}

public enum NodeState
{
    Clean,
    Weakened,
    Infected,
    Flagged,
}

public int Solve(IEnumerable<string> input, int iterations, int part)
{
    // Build the grid
    var points = input.SelectMany((l, li) => 
        l.ToCharArray()
            .Select((c, ci) =>
                new { P = new Point(ci, -li), S = c == '#' ? NodeState.Infected : NodeState.Clean })
            .Where(p => p.S == NodeState.Infected) // optional, only store infected to start with
            )
        .ToDictionary(x => x.P, x => x.S);
    
    var move = new Point(0,1);
    var current = new Point((input.Count() / 2), - (input.Count() / 2));
    var icount = 0;
    for (var index = 0; index < iterations; index++)
    {
        var state = points.ContainsKey(current) ? points[current] : NodeState.Clean;
            
        switch (state)
        {
            case NodeState.Clean: // turn left
                move = new Point(-move.Y, move.X);
                points[current] = part == 1 ? NodeState.Infected : NodeState.Weakened;
                icount += part == 1 ? 1 : 0; // infection count for part 1 only
                break;
            case NodeState.Weakened: // do not turn
                points[current] = NodeState.Infected;
                icount += 1; // infection count for part 2 (part 1 will never get here)
                break;
            case NodeState.Infected: // turn right
                move = new Point(move.Y, -move.X);
                points[current] = part == 1 ? NodeState.Clean : NodeState.Flagged;
                break;
            case NodeState.Flagged: // reverse direction
                move = new Point(-move.X, -move.Y);
                points[current] = NodeState.Clean;
                break;
        }
        current = new Point(current.X + move.X, current.Y + move.Y);
    }
    return icount;
}
