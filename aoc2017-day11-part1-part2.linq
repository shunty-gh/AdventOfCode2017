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

// Could (should?) use proper graph traversal techniques for this but in this
// case simple works perfectly well enough.

public int DistanceFromOrigin(Point current)
{
	int result;
	if (Math.Abs(current.X) > Math.Abs(current.Y))
	{
		result = Math.Abs(current.X);
	}
	else
	{
		result = Math.Min(Math.Abs(current.X), Math.Abs(current.Y)) + (Math.Abs(Math.Abs(current.X) - Math.Abs(current.Y)) / 2);
	}
	return result;
}

public Tuple<int, int> Solve(string input)
{
	/*
	Using a hexagonal grid with x,y co-ords of the form
	   
	   -4,2      -2,2        0,2        2,2      4,2
            -3,1       -1,1       1,1        3,1
	   -4,0      -2,0        0,0        2,0      4,0    
	       -3,-1      -1,-1      1,-1       3,-1
	   -4,-2     -2,-2      0,-2       2,-2     4,-2
	*/
	
	var furthest = 0;
    var directions = input.Split(',').ToList();
	var current = new Point(0, 0);
	foreach (var direction in directions)
	{
		switch (direction)
		{
			case "n":
				current.Y += 2;
				break;
			case "ne":
				current.X += 1;
				current.Y += 1;
				break;
			case "se":
				current.X += 1;
				current.Y -= 1;
				break;
			case "s":
				current.Y -= 2;
				break;
			case "sw":
				current.X -= 1;
				current.Y -= 1;
				break;
			case "nw":
				current.X -= 1;
				current.Y += 1;
				break;
		}
		var distance = DistanceFromOrigin(current);
		if (distance > furthest)
		{
			furthest = distance;
		}
	}
	return new Tuple<int, int>(DistanceFromOrigin(current), furthest);
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