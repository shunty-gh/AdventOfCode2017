<Query Kind="Program" />

void Main()
{
	// Advent of code 2017
	// Day 10, part 2

    var input = "192,69,168,160,78,1,166,28,0,83,198,2,254,255,41,12";
	//var input = "1,2,3";  // "3efbe78a8d82f29979031a4aa0b16a9d"
	//var input = ""; // expect "a2582a3a0e66e6e86e3812dcb672a272"
	//var input = "1,2,4"; // expect "63960835bcdc130f0b66d7ff4f6a5a8e"
	//var input = "AoC 2017"; // expect "33efeb34ea91902bb2f59c9920caa6cd"
	var result = Solve(input);
    Console.WriteLine($"Result: {result}");
}

public string Solve(string input)
{
	//var nums = input..Select(c => (int)c).ToList();
	var nums = Encoding.ASCII.GetBytes(input).ToList();
	nums.AddRange(new byte[] { 17, 31, 73, 47, 23 });
	var list = Enumerable.Range(0, 256).Select(x => x).ToList();
	var listlen = 256;
	var skipsize = 0;
	var current = 0;

	for (var round = 0; round < 64; round++)
	{
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
		}
	}	
	
	var blocks = new int[16];
	var hash = 0;
	for (var index = 0; index < 256; index++)
	{
		hash ^= list[index];
		if ((index + 1) % 16 == 0)
		{
			blocks[((index + 1) / 16) - 1] = hash;
			hash = 0;
		}
	}
	// to hex string
	var result = "";
	for (var index = 0; index < 16; index++)
	{
		result += blocks[index].ToString("x2");
	}
	return result;
}