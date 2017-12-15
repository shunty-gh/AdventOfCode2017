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
            // Reverse section of num elements
            for (var index = 0; index < (num / 2); index++)
            {
                int a = (current + index) % listlen;
                int b = (current + num - index - 1) % listlen;
                var tmp = list[a];
                list[a] = list[b];
                list[b] = tmp;                
            }
            current = (current + num + skipsize) % listlen;
            skipsize++;
        }
    }

    var blocks = new int[16];
    for (var blockindex = 0; blockindex < 16; blockindex++)
    {
        blocks[blockindex] = list.Skip(blockindex * 16).Take(16).Aggregate(0, (h, b) => h ^= b);
    }
    // to hex string
    var result = blocks.Aggregate("", (s, b) => s += b.ToString("x2"));
    return result;
}