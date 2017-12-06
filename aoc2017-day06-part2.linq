<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 6, part 2
    int day = 6;
    int part = 1;

    if (!RunTests())
        return;

    var input = ReadTextForDay(day, part).Replace("  ", " ").Replace('\t', ' ').Split(' ').Select(s => int.Parse(s)).ToList();
    var result = Solve(input);
    Console.WriteLine($"Result: {result}");
}

public int Solve(IList<int> input)
{
    var round = 0;
    var inlen = input.Count;
    var snapshots = new Dictionary<string, int>();
    while (true)
    {
        var snapshot = string.Join("_", input);
        //Console.WriteLine($"Round {result}; Snapshot: {snapshot}");
        if (snapshots.ContainsKey(snapshot))
        {
            return round - snapshots[snapshot];
        }
        snapshots.Add(snapshot, round);
        // Reallocate
        var largest = input.Max();
        var indexoflargest = input.IndexOf(largest);
        var nextindex = indexoflargest;
        input[indexoflargest] = 0;
        while (largest > 0)
        {
            nextindex = (nextindex + 1) % inlen;
            input[nextindex] += 1;
            largest--;
        }
        round++;
    }
}

public bool RunTests()
{
    var result = true;
    var testinput = new List<int> {
        0,2,7,0
    };
    var expected = 4;
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

public string GetInputFilename(int dayNumber, int part)
{
    return Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{dayNumber:D2}-part{part:D1}.txt");
}