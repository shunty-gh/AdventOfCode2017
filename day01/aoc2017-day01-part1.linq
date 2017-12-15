<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 1, part 1
    int day = 1;
    int part = 1;
    
    RunTests();
    var input = ReadTextForDay(day, part);
    var result = Solve(input);
    result.Dump(result);
}

public class TestInput
{
    public string Source { get; set; }
    public int Solution { get; set; }
}

public void RunTests()
{
    var testinput = new List<TestInput> {
        new TestInput { Source = "1122", Solution = 3 },
        new TestInput { Source = "1111", Solution = 4 },
        new TestInput { Source = "1234", Solution = 0 },
        new TestInput { Source = "91212129", Solution = 9 },
    };
    var tindex = 0;
    foreach (var ti in testinput)
    {
        tindex++;
        var testresult = Solve(ti.Source);
        System.Diagnostics.Debug.Assert(ti.Solution == testresult, $"Expected {ti.Solution} for test {tindex} but got {testresult}.");
    }
}

public int Solve(string input)
{
    var sum = 0;
    for (var index = 0; index < input.Length; index++)
    {
        var ch = input[index];
        var nextch = index == input.Length - 1 ? input[0] : input[index + 1];
        if (ch == nextch)
        {
            sum += (int)(ch - '0');
        }
    }
    return sum;
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