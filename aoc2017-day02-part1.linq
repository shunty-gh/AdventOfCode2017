<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 2, part 1
    int day = 2;
    int part = 1;

    if (!RunTests())
	{
		Console.WriteLine("Tests failed. Stopping.");
		return;
	}

    var input = ReadLinesForDay(day, part);
	var result = Solve(input);
	Console.WriteLine($"Result is {result}");
}

public int Solve(IEnumerable<string> input)
{
	var result = 0;
	foreach (var line in input)
	{
		int min = int.MaxValue, max = 0;
		var nums = line.Split(new[] {'\t', ' ' });
		foreach (var num in nums)
		{
			var inum = int.Parse(num);
			if (inum < min)
			{
				min = inum;
			} 
			if (inum > max)
			{
				max = inum;
			}
		}
		result += (max - min);
	}
    return result;
}

public bool RunTests()
{
    var result = true;
    var testinput = new List<string> {
        "5 1 9 5",
        "7 5 3",
		"2 4 6 8"
    };
    var testresult = Solve(testinput);
    if (testresult != 18) result = false;
    System.Diagnostics.Debug.Assert(18 == testresult, $"Expected 18 for test but got {testresult}.");
    
    return result;
}

public IEnumerable<string> ReadLinesForDay(int dayNumber, int part)
{
    var fname = GetInputFilename(dayNumber, part);
    var result = File.ReadAllLines(fname, Encoding.UTF8);
    return result;
}

public string GetInputFilename(int dayNumber, int part)
{
    return Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{dayNumber:D2}-part{part:D1}.txt");
}