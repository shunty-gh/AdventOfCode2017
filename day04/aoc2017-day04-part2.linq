<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 4, part 2
    int day = 4;
    int part = 1; // Same data as part 1

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
        var lineok = true;
        var parts = line.Split(' ').ToList();
        // Sort the chars in each word 
        var sortedparts = parts.Select(p => string.Concat(p.ToCharArray().OrderBy(c => c)))
            .OrderBy(s => s)
            .ToList();
        //sortedparts.Dump();
        for (var index = 0; index < sortedparts.Count() - 1; index++)
        {
            if (sortedparts[index] == sortedparts[index + 1])
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

public bool RunTests()
{
    var testinput = new List<string> {
        "abcde fghij", //true
        "abcde xyz ecdab", //false
        "a ab abc abd abf abj", //true
        "iiii oiii ooii oooi oooo", //true
        "oiii ioii iioi iiio", // false
    };
    var expected = 3;
    var result = Solve(testinput);
    System.Diagnostics.Debug.Assert(expected == result, $"Expected {expected} valid but got {result}");
    return result == expected;
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