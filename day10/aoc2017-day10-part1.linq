<Query Kind="Program" />

void Main()
{
	// Advent of code 2017
	// Day 10, part 1

	//var input = "3,4,1,5"; // list = 0,1,2,3,4; expect 3 x 4 = 12
    var input = "192,69,168,160,78,1,166,28,0,83,198,2,254,255,41,12";
	var result = Solve(input, 256);
    Console.WriteLine($"Result: {result}");
}

public int Solve(string input, int length)
{
	var nums = input.Split(',').Select(c => int.Parse(c)).ToList();
	var list = Enumerable.Range(0, length).Select(x => x).ToList();
	var listlen = length;
	//list.Dump();
	var skipsize = 0;
	var current = 0;
	
	foreach (var num in nums)
	{
		// so many better ways of doing this. but it works, for now
		var sublist = new List<int>(num);
		// Select num elements and reverse them
		for (var index = 0; index < num; index++)
		{
			sublist.Add(list[(current + index) % listlen]);
		}
		sublist.Reverse();
		for (var index = 0; index < num; index++)
		{
			list[(current + index) % listlen] = sublist[index];
		}
		current = (current + num + skipsize) % listlen;
		skipsize++;
		//list.Dump();
	}
	
	list[0].Dump();
	list[1].Dump();
	return list[0] * list[1];
}
