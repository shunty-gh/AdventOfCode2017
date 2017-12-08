<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day X, part X
    // Template. CLONE this before saving.
    int day = 0;
    int part = 1;

    if (!RunTests())
        return;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}-part{part:D1}.txt");
    //var input = File.ReadAllText(inputname, Encoding.UTF8);
    //...or...
    //var input = File.ReadAllLines(inputname, Encoding.UTF8);
    var result = Solve(input);
    Console.WriteLine($"Result: {result}");
}

public int Solve(IEnumerable<string> input)
{
    // Work it out
    return 0;
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
