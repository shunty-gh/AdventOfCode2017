<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 9, part 2

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day09-part1.txt");
    var input = File.ReadAllText(inputname, Encoding.UTF8);
	var result = Solve(input);
	Console.WriteLine($"Group score: {result.Item1}; Garbage count: {result.Item2}");
}

public Tuple<int, int> Solve(string input)
{
	var groupcount = 0;
	var groupscore = 0;
	var grouplevel = 1;
	var garbagecount = 0;
	for (var index = 0; index < input.Length; index++)
	{
		var ch = input[index];
		if (ch == '{')
		{
			groupcount++;
			groupscore += grouplevel;
			grouplevel++;
		}
		else if (ch == '}')
		{
			grouplevel -= 1;
		}
		else if (ch == '<')
		{
			index++;
			// Skip over the rest of the garbage
			while (index < input.Length)
			{
				var g = input[index];
				if (g == '!')
				{
					index++;
				}
				else if ((input[index] == '>'))
				{
					break;
				} else 
				{
					garbagecount++;
				}
				index++;
			}
		}
		else if (ch == '!')
		{
			index += 1;
		}
	}
    return new Tuple<int, int>(groupscore, garbagecount);
}
