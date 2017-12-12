<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 12
    int day = 12;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);
    var result = Solve(input);
    Console.WriteLine($"Result, part 1: {result.Item1}");
    Console.WriteLine($"Result, part 2: {result.Item2}");
}

public struct Program
{
    public int Id;
    public IEnumerable<int> Connections;
}

public void VisitGroup(Dictionary<int, Program> programs, IList<int> visited, int groupKey)
{
    //Console.WriteLine($"Grouping {groupKey}");
    var tovisit = new Queue<int>();
    tovisit.Enqueue(groupKey);
    int current;
    while (tovisit.Count > 0)
    {
        current = tovisit.Dequeue();
        if (!visited.Contains(current))
        {
            visited.Add(current);
            foreach (var connection in programs[current].Connections)
            {
                if (!visited.Contains(connection))
                {
                    tovisit.Enqueue(connection);
                }
            }
        }
    }
}

public Tuple<int, int> Solve(IEnumerable<string> input)
{
    var programs = input.Select(s =>
    {
        var splits = s.Split(new[] { " ", ", " }, StringSplitOptions.RemoveEmptyEntries);
        return new Program
        {
            Id = int.Parse(splits[0]),
            Connections = new List<int>(splits.Skip(2).Select(c => int.Parse(c)))
        };
    })
    .ToDictionary(p => p.Id);
    //programs.Dump();

    var visited = new List<int>();
    VisitGroup(programs, visited, 0);
    var groupzero = visited.Count;
    var groupcount = 1;

    foreach (var prog in programs)
    {
        if (!visited.Contains(prog.Key))
        {
            VisitGroup(programs, visited, prog.Key);
            groupcount++;
        }
    }
    System.Diagnostics.Debug.Assert(input.Count() == visited.Count, $"Should have visited {input.Count()} but actually visited {visited.Count}");
    return new Tuple<int, int>(groupzero, groupcount);
}
