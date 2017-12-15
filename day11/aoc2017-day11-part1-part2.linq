<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
    // Advent of code 2017
    // Day 11
    int day = 11;

    if (!RunTests())
        return;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllText(inputname, Encoding.UTF8);
    var result = Solve(input);
	Console.WriteLine($"Final distance: {result.Item1}");
	Console.WriteLine($"Furthest distance: {result.Item2}");
}

// Using cube co-ordinates for a hexagonal grid
// https://www.redblobgames.com/grids/hexagons/

public struct HexPoint
{
	public int X;
	public int Y;
	public int Z;
	
	public HexPoint(int x, int y, int z)
	{
		X = x;
		Y = y;
		Z = z;
	}
	
	public int DistanceFromOrigin
	{
		get
		{
			return (Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z)) / 2;
		}
	}
}

public Tuple<int, int> Solve(string input)
{
	var furthest = 0;
    var directions = input.Split(',').ToList();
	var current = new HexPoint(0, 0, 0);
	foreach (var direction in directions)
	{
		switch (direction)
		{
			case "n":
				current.Y += 1;
				current.Z -= 1;
				break;
			case "ne":
				current.X += 1;
				current.Z-= 1;
				break;
			case "se":
				current.X += 1;
				current.Y -= 1;
				break;
			case "s":
				current.Y -= 1;
				current.Z += 1;
				break;
			case "sw":
				current.X -= 1;
				current.Z += 1;
				break;
			case "nw":
				current.X -= 1;
				current.Y += 1;
				break;
		}
		var distance = current.DistanceFromOrigin;
		if (distance > furthest)
		{
			furthest = distance;
		}
	}
	return new Tuple<int, int>(current.DistanceFromOrigin, furthest);
}

public bool RunTests()
{
    var result = true;
    var testinput = new List<string> {
        "ne,ne,ne", 
		"ne,ne,sw,sw",
		"ne,ne,s,s",
		"se,sw,se,sw,sw"
	};
	var index = 0;
	var expected = new List<int> { 3, 0, 2, 3 };
	foreach (var test in testinput)
	{
		var testresult = Solve(test);
		if (testresult.Item1 != expected[index]) result = false;
		System.Diagnostics.Debug.Assert(expected[index] == testresult.Item1, $"Expected {expected[index]} but got {testresult.Item1}.");
		index++;
	}
    return result;
}