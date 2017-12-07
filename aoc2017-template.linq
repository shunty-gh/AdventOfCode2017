<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Template. CLONE this before saving.
    int day = 0;
    int part = 1;

    if (!RunTests())
        return;

    var input = ReadLinesForDay(day, part);
    var result = Solve(input);
    Console.WriteLine($"Result: {result}");
}

public int Solve(IEnumerable<string> input)
{
    // Work it out
    return 0;
}

public class TestInput
{
    public string Source { get; set; }
    public int Solution { get; set; }
}

public bool RunTests()
{
    var result = true;
    var testinput = new List<string> {
        "", ""
    };
    var expected = 0;
    var testresult = Solve(testinput);
    if (testresult != expected) result = false;
    System.Diagnostics.Debug.Assert(expected == testresult, $"Expected {expected} but got {testresult}.");
    return result;
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