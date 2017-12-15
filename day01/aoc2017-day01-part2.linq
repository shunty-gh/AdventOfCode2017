<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 1, part 2
    int day = 1;
    int part = 2;
    
    if (!RunTests())
        return;
        
    var input = ReadTextForDay(day, part);
    var result = Solve(input);
    result.Dump(result);
}

public int Solve(string input)
{
    var sum = 0;
    var inputlength = input.Length;
    var step = inputlength / 2;  // List has an even number of elements
    for (var index = 0; index < inputlength; index++)
    {
        var nextindex = index + step;
        if (nextindex >= inputlength)
            nextindex -= inputlength;
            
        var ch = input[index];
        var nextch = input[nextindex];
        if (ch == nextch)
        {
            sum += (int)(ch - '0');
        }
    }
    return sum;
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
        new TestInput { Source = "1212", Solution = 6 },
        new TestInput { Source = "1221", Solution = 0 },
        new TestInput { Source = "123425", Solution = 4 },
        new TestInput { Source = "123123", Solution = 12 },
        new TestInput { Source = "12131415", Solution = 4 },
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