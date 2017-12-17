<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 17

    var input = 367;
	//var input = 3;  // Test input => part 1: 638
    var result = Solve(input);
	Console.WriteLine($"Result, part 1: {result.Item1}");
	Console.WriteLine($"Result, part 2: {result.Item2}");
}

public Tuple<int, int> Solve(int input)
{
	var buffer = new List<int>(2017) { 0 };
	var insertion = 0;
	var current = 0;
	
	// Part 1
	while (insertion < 2017)
	{
		insertion++;
		current = ((current + input) % buffer.Count) + 1;
		if (current == buffer.Count)
		{
			buffer.Add(insertion);
		}
		else
		{
			buffer.Insert(current, insertion);
		}
		//string.Join(",", buffer).Dump();
	}
	var result1 = buffer[current + 1];

	//System.Diagnostics.Debug.Assert(2018 == buffer.Count, "Buffer count is wrong");
	//System.Diagnostics.Debug.Assert(2017 == insertion, "Insertion is wrong");
	
	// No need to do all the list allocations and insertions etc, just track 
	// the element at index 1 as '0' will always be at index 0
	var bcount = buffer.Count;
	var result2 = buffer[1];
	while (insertion < 50_000_000)
	{
		insertion++;
		current = ((current + input) % bcount) + 1;
		if (current == 1)
			result2 = insertion;
		bcount++;
	}

	return new Tuple<int, int>(result1, result2);
}