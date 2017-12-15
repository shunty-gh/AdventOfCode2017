<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 9, part 1
    int day = 9;
    int part = 1;

    if (!RunTests())
        return;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}-part{part:D1}.txt");
    var input = File.ReadAllText(inputname, Encoding.UTF8);
    var result = Solve(input);
    Console.WriteLine($"Result: {result}");
}

public int Solve(string input)
{
	var groupcount = 0;
	var groupscore = 0;
	var grouplevel = 1;
	var garbage = false;
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
			garbage = true;
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
					garbage = false;
					break;
				}
				index++;
			}
		}
		else if (ch == '!')
		{
			index += 1;
		}
	}
	
    return groupscore;
}

public bool RunTests()
{
    var result = true;
    var testinput = new List<string> {
        "{{{},{},{{}}}}",
		"{{<ab>},{<ab>},{<ab>},{<ab>}}",
		"{{<!!>},{<!!>},{<!!>},{<!!>}}",
		"{{<a!>},{<a!>},{<a!>},{<ab>}}"
    };
    var expected = new List<int> {16, 9, 9, 3};

	var index = 0;
	foreach (var test in testinput)
	{
		var testresult = Solve(test);
		if (testresult != expected[index]) result = false;
		System.Diagnostics.Debug.Assert(expected[index] == testresult, $"Expected {expected[index]} but got {testresult}, test: {index + 1}.");
		index++;
	}
	return result;
}