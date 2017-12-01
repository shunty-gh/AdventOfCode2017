<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Template. CLONE this before saving.
    int day = 0;
    int part = 1;

    if (!RunTests())
        return;

    var input = ReadTextForDay(day, part);
    var result = Solve(input);
}

public int Solve(string input)
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
    var testinput = new List<TestInput> {
        new TestInput { Source = "", Solution = 0 },
        new TestInput {  },
    };
    var tindex = 0;
    foreach (var ti in testinput)
    {
        tindex++;
        var testresult = Solve(ti.Source);
        if (testresult != ti.Solution) result = false;
        System.Diagnostics.Debug.Assert(ti.Solution == testresult, $"Expected {ti.Solution} for test {tindex} but got {testresult}.");
    }
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