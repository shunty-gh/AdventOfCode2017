<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 23
    int day = 23;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);
    var result = Solve(input);
    Console.WriteLine($"Result: {result}");
}

public int Solve(IEnumerable<string> input)
{
	var registers = new Dictionary<string, Int64> {
		{ "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 }, { "e", 0 }, { "f", 0 }, { "g", 0 }, { "h", 0 }, 
	};
	var inst = input.Select(s => s.Split(' ')).ToList().AsReadOnly();
	var index = 0;
	var mcount = 0;
	while (index >= 0 && index < input.Count())
	{
		var instruction = inst[index];
		var regx = instruction[1];
		switch(instruction[0])
		{
			case "set":
				registers[regx] = GetInstructionValue(registers, instruction[2]);
				index++;
				break;
			case "sub":
				registers[regx] -= GetInstructionValue(registers, instruction[2]);
				index++;
				break;
			case "mul":
				mcount += 1;
				registers[regx] *= GetInstructionValue(registers, instruction[2]);
				index++;
				break;
			case "jnz":
				index += GetInstructionValue(registers, regx) != 0 
					? (int)GetInstructionValue(registers, instruction[2]) 
					: 1;
				break;
		}
	}
    return mcount;
}

public Int64 GetInstructionValue(IDictionary<string, Int64> registers, string keyOrValue)
{
	Int64 result;
	if (!Int64.TryParse(keyOrValue, out result))
	{
		if (!registers.ContainsKey(keyOrValue))
		{
			registers[keyOrValue] = 0;
		}
		result = registers[keyOrValue];
	}
	return result;
}
