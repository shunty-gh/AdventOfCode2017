<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 4, part1
    int day = 4;
    int part = 1;

    if (!RunTests())
        return;

    var input = ReadLinesForDay(day, part);
    var result = Solve(input);
    Console.WriteLine($"Result is {result}");
}

public int Solve(IEnumerable<string> input)
{
    var result = 0;
    foreach (var line in input)
    {
        var parts = line.Split(' ').OrderBy(p => p).ToList();
        var lineok = true;
        for (var index = 0; index < parts.Count() - 1; index++)
        {
            if (parts[index] == parts[index + 1])
            {
                lineok = false;
                break;
            }
        }
        if (lineok)
            result++;
    }
    return result;
}

public class TestInput
{
    public string Source { get; set; }
    public bool Solution { get; set; }
}

public bool RunTests()
{
    var testinput = new List<string> {
        "aa bb cc dd ee", //true
        "aa bb cc dd aa", //false
        "aa bb cc dd aaa", //true
    };
    var expected = 2;
    var result = Solve(testinput);
    System.Diagnostics.Debug.Assert(expected == result, $"Expected 2 valid but got {result}");
    return result == expected;
}

public string ReadTextForDay(int dayNumber, int part)
{
    var fname = GetInputFilename(dayNumber, part);
    var result = File.ReadAllText(fname, Encoding.UTF8);
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