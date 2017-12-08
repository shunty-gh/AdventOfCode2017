<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 8, parts 1 and 2
    int day = 8;
    int part = 1; 
    
    if (!RunTests())
        return;

    var input = ReadLinesForDay(day, part);
    var result = Solve(input);
    Console.WriteLine($"Result, part1: {result.Item1}");
    Console.WriteLine($"Result, part2: {result.Item2}");
}

public static string pattern = @"^(?<register>\w*)\s(?<decinc>(dec|inc))\s(?<amount>-{0,1}\d*)\sif\s(?<condreg>\w*)\s(?<condop>.*)\s(?<condval>-{0,1}\d*)$";
public static Regex re = new Regex(pattern);

public enum RegisterConditionOp
{
    Unknown,
    Equals, 
    NotEquals,
    LessThan,
    GreaterThan,
    LessThanOrEqual,
    GreaterThanOrEqual
}

public static RegisterConditionOp OpFromString(string source)
{
    var s = source.Trim().ToLower();
    if (s == "==")
        return RegisterConditionOp.Equals;
    else if (s == "!=")
        return RegisterConditionOp.NotEquals;
    else if (s == ">")
        return RegisterConditionOp.GreaterThan;
    else if (s == "<")
        return RegisterConditionOp.LessThan;
    else if (s == ">=")
        return RegisterConditionOp.GreaterThanOrEqual;
    else if (s == "<=")
        return RegisterConditionOp.LessThanOrEqual;
    else
        return RegisterConditionOp.Unknown;
}

public struct RegisterCondition
{
    public string Register;
    public RegisterConditionOp Operator;
    public int Amount;
}

public struct RegisterInstruction
{
    public string Register;
    public int IncDec;
    public int Amount;
}

public Tuple<int, int> Solve(IEnumerable<string> input)
{
    var highest = 0;
    var registers = new Dictionary<string, int>();
    foreach (var line in input)
    {
        // Parse each input line into a RegisterCondition and RegisterInstruction
        var match = re.Match(line);
        var rc = new RegisterCondition
        {
            Register = match.Groups["condreg"].Value,
            Operator = OpFromString(match.Groups["condop"].Value),
            Amount = int.Parse(match.Groups["condval"].Value),
        };
        var ri = new RegisterInstruction
        {
               Register = match.Groups["register"].Value,
               IncDec = match.Groups["decinc"].Value == "dec" ? -1 : 1,
               Amount = int.Parse(match.Groups["amount"].Value),
        };
        //rc.Dump();
        //ri.Dump();
        
        // Make sure the registers are in the dictionary
        if (!registers.ContainsKey(ri.Register))
            registers.Add(ri.Register, 0);
        if (!registers.ContainsKey(rc.Register))
            registers.Add(rc.Register, 0);
        
        // Check if the condition is true
        var applyit = false;
        switch (rc.Operator)
        {
            case RegisterConditionOp.Equals:
                applyit = (registers[rc.Register] == rc.Amount);
                break;
            case RegisterConditionOp.NotEquals:
                applyit = (registers[rc.Register] != rc.Amount);
                break;
            case RegisterConditionOp.GreaterThan:
                applyit = (registers[rc.Register] > rc.Amount);
                break;
            case RegisterConditionOp.LessThan:
                applyit = (registers[rc.Register] < rc.Amount);
                break;
            case RegisterConditionOp.GreaterThanOrEqual:
                applyit = (registers[rc.Register] >= rc.Amount);
                break;
            case RegisterConditionOp.LessThanOrEqual:
                applyit = (registers[rc.Register] <= rc.Amount);
                break;
        }
        // Apply it if appropriate
        if (applyit)
        {
            registers[ri.Register] += (ri.Amount * ri.IncDec);
            var current = registers[ri.Register];
            if (current > highest)
            {
                highest = current;
            }
        }
            
    }
    return new Tuple<int, int>(registers.Max(r => r.Value), highest);
}

public bool RunTests()
{
    var result = true;
    var testinput = new List<string> {
        "b inc 5 if a > 1",
        "a inc 1 if b < 5",
        "c dec -10 if a >= 1",
        "c inc -20 if c == 10"
    };
    var expectedA = 1;
    var expectedB = 10;
    var testresult = Solve(testinput);
    if ((testresult.Item1 != expectedA) ||(testresult.Item2 != expectedB)) result = false;
    System.Diagnostics.Debug.Assert(expectedA == testresult.Item1, $"Expected {expectedA} but got {testresult.Item1} for part 1.");
    System.Diagnostics.Debug.Assert(expectedB == testresult.Item2, $"Expected {expectedB} but got {testresult.Item2} for part 2.");
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