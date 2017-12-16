<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 16.
    int day = 16;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
	var input = File.ReadAllText(inputname, Encoding.UTF8);
    var result = Solve(input);
	Console.WriteLine($"Result, part 1: {result.Item1}");
	Console.WriteLine($"Result, part 2: {result.Item2}");
}

public Tuple<string, string> Solve(string input)
{
	var moves = input.Split(',');
	var dancersorg = Enumerable.Range(0, 16).Select(i => (char)('a' + i));
	var dancers = dancersorg.ToList();
	var result1 = "";
	var looped = 0;

	// Find how many iterations to come full circle
	while (true)
	{
		Dance(dancers, moves);
		looped++;
		
		// Part 1
		if (looped == 1)
			result1 = string.Join("", dancers);
			
		// Are we back to square 1 yet
		if (dancersorg.Zip(dancers, (c1, c2) => c1 == c2).All(c => c))
		{
			Console.WriteLine($"Full loop after {looped} iterations");
			break;
		}
	}

	// Do the rest of the rounds
	var rounds = 1_000_000_000 % looped;
	while (rounds-- > 0)
	{
		Dance(dancers, moves);
	}
	var result2 = string.Join("", dancers);
	return new Tuple<string, string>(result1, result2);
}

public void Dance(List<char> dancers, IEnumerable<string> moves)
{
	var dcount = dancers.Count;

	foreach (var move in moves)
	{
		switch (move[0])
		{
			case 's':
				var swaps = int.Parse(move.Substring(1));
				var items = dancers.Skip(dcount - swaps).Take(swaps).ToList();
				dancers.RemoveRange(dcount - swaps, swaps);
				dancers.InsertRange(0, items);
				break;
			case 'x':
				var xplaces = move.Substring(1).Split('/');
				var xfrom = int.Parse(xplaces[0]);
				var xto = int.Parse(xplaces[1]);
				var xtmp = dancers[xfrom];
				dancers[xfrom] = dancers[xto];
				dancers[xto] = xtmp;
				break;
			case 'p':
				var pplaces = move.Substring(1).Split('/');
				var pfrom = dancers.IndexOf(pplaces[0][0]);
				var pto = dancers.IndexOf(pplaces[1][0]);
				dancers[pfrom] = pplaces[1][0];
				dancers[pto] = pplaces[0][0];
				break;
			default:
				Console.WriteLine($"Unknown move type '{move[0]}'");
				break;
		}
	}
}
