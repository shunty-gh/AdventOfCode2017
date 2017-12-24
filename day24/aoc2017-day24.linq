<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 24
	// Could probably be more efficient but it works
    int day = 24;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);
    var result = Solve(input);
    Console.WriteLine($"Result: {result}");
}

public (int, int) Solve(IEnumerable<string> input)
{
	var comps = input.Select((c, i) => (P1: int.Parse(c.Trim().Split('/')[0]), P2: int.Parse(c.Trim().Split('/')[1]), Index: i)).ToList();
	
	var q = new Queue<List<(int From, int To, int Index)>>();
	var paths = new List<List<(int From, int To, int Index)>>();
	var starts = comps.Where(c => c.P1 == 0 || c.P2 == 0)
		.Select(c => new List<(int From, int To, int Index)> { (From: c.P1 == 0 ? c.P1 : c.P2, To: c.P1 == 0 ? c.P2 : c.P1, Index: c.Index) })
		.ToList(); 
	starts.ForEach(x => q.Enqueue(x));

	while (q.Count > 0)
	{
		var current = q.Dequeue();
		var to = current.Last().To;
		var matches = comps.Where(c => !current.Any(cc => cc.Index == c.Index) && (c.P1 == to || c.P2 == to));
		if (matches.Count() == 0)
		{
			paths.Add(current);
		}
		foreach (var comp in matches)
		{
			var c2 = new List<(int From, int To, int Index)>(current);
			c2.Add((From: comp.P1 == to ? comp.P1 : comp.P2, To: comp.P1 == to ? comp.P2 : comp.P1, Index: comp.Index));
			q.Enqueue(c2);
		}
	}
    var part1 = paths.Max(p => p.Sum(pp => pp.From + pp.To));
	
	var p2 = paths.OrderByDescending(p => p.Count).ThenByDescending(p => p.Sum(pp => pp.From + pp.To));
	return (part1, p2.First().Sum(pp => pp.From + pp.To));
}

