<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 2, part 2
    int day = 2;
    int part = 1; // input is same for p1 and p2

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
		int divresult = 0;
		var nums = line.Split(new[] {'\t', ' ' }).Select(n => int.Parse(n)).ToList();
		for (var index = 0; index < nums.Count; index++)
		{
			var num = nums[index];
			for (var innerindex = index + 1; innerindex < nums.Count; innerindex++)
			{
				var innernum = nums[innerindex];
				if (num % innernum == 0)
				{
					divresult = num / innernum;
				} 
				else if (innernum % num == 0)
				{
					divresult = innernum / num;
				}
				if (divresult > 0) break;
			}
		}
		result += divresult;
	}
    return result;
}

public bool RunTests()
{
    var result = true;
    var testinput = new List<string> {
        "5 9 2 8",
        "9 4 7 3",
		"3 8 6 5"
    };
	var expected = 9;
    var testresult = Solve(testinput);
	if (testresult != expected) result = false;
	System.Diagnostics.Debug.Assert(expected == testresult, $"Expected {expected} for test but got {testresult}.");
    
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