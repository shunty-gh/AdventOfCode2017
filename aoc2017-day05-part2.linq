<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 5, part 2
    int day = 5;
    int part = 1; // input is the same

    if (!RunTests())
        return;

    var input = ReadLinesForDay(day, part).Select(l => int.Parse(l)).ToArray();
    var result = Solve(input);
    Console.WriteLine($"Result {result}");
}

public int Solve(int[] input)
{
    var steps = 0;
    var index = 0;
    var target = input.Count();
    while (index < target)
    {
        steps++;
        var current = input[index];
        input[index] += current >= 3 ? -1 : +1;
        index += current;
    }
    return steps;
}

public bool RunTests()
{
    var result = true;
    var testinput = new int[] {
        0, 3, 0, 1, -3
    };
    var expected = 10;
    var testresult = Solve(testinput);
    if (testresult != expected) result = false;
    System.Diagnostics.Debug.Assert(expected == testresult, $"Expected {expected} but got {testresult}.");
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