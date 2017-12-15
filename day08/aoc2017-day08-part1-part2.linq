<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 8, parts 1 and 2
    
    if (!RunTests())
        return;

    var fname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day08-part1.txt");
    var input = File.ReadAllLines(fname, Encoding.UTF8);
    
    var result = Solve(input);

    Console.WriteLine($"Result, part1: {result.Item1}");
    Console.WriteLine($"Result, part2: {result.Item2}");
}

public static string pattern = @"^(?<register>\w*)\s(?<decinc>(dec|inc))\s(?<amount>-{0,1}\d*)\sif\s(?<condreg>\w*)\s(?<condop>.*)\s(?<condval>-{0,1}\d*)$";
public static Regex re = new Regex(pattern);

public struct RegisterCondition
{
    public string Register;
    public string Operator;
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
            Operator = match.Groups["condop"].Value.Trim(),
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
        
        // Check if the condition is true. A fully fledged expression parser/evaulator 
        // is a bit much just for this script. Simple is good.
        var applyit = false;
        switch (rc.Operator)
        {
            case "==":
                applyit = (registers[rc.Register] == rc.Amount);
                break;
            case "!=":
                applyit = (registers[rc.Register] != rc.Amount);
                break;
            case ">":
                applyit = (registers[rc.Register] > rc.Amount);
                break;
            case "<":
                applyit = (registers[rc.Register] < rc.Amount);
                break;
            case ">=":
                applyit = (registers[rc.Register] >= rc.Amount);
                break;
            case "<=":
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
