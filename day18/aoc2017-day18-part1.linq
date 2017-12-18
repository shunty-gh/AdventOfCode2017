<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 18
    int day = 18;

    if (!RunTests())
        return;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);
    var result = Solve(input);
    Console.WriteLine($"Result: {result}");
}

public Int64 Solve(IEnumerable<string> input)
{
    var instructions = input.Select(s => s.Split(' ')).ToList();
    var registers = new Dictionary<char, Int64>();
    Int64 lastsnd = 0;
    Int64 result1 = 0;
    var recovered = false;
    var index = 0;
    
    while (!recovered)
    {
        var instruction = instructions[index];
        var action = instruction[0];
        var tmp = instruction[1];
        char regX = '_';
        Int64 regXvalue = 0;
        if (!Int64.TryParse(tmp, out regXvalue))
        {
            regX = tmp[0];
            if (!registers.ContainsKey(regX))
                registers[regX] = 0;
            regXvalue = registers[regX];
        }
        char regY = '_';
        Int64 regYvalue = 0;
        if (instruction.Count() > 2)
        {
            tmp = instruction[2];
            if (!Int64.TryParse(tmp, out regYvalue))
            {
                regY = tmp[0];
                if (!registers.ContainsKey(regY))
                    registers[regY] = 0;
                regYvalue = registers[regY];
            }
        }
        //Console.WriteLine($"Index: {index}; {action}; X: {regX} = {regXvalue}; Y: {regY} = {regYvalue}");
        switch (action)
        {
            case "snd":
                lastsnd = regXvalue;
                index++;
                break;
            case "set":
                registers[regX] = regYvalue;
                index++;
                break;
            case "add":
                registers[regX] += regYvalue;
                index++;
                break;
            case "mul":
                registers[regX] *= regYvalue;
                index++;
                break;
            case "mod":
                registers[regX] %= regYvalue;
                index++;
                break;
            case "rcv":
                if (regXvalue > 0)
                {
                    result1 = lastsnd;
                    recovered = true;
                }
                index++;
                break;
            case "jgz":
				index += (int)(regXvalue > 0 ? regYvalue : 1);
                break;
            default:
                throw new Exception($"Unknown instruction: {action}");
        }
        //registers.Dump();
    }
    return result1;
}

public bool RunTests()
{
    var result = true;
    var testinput = new List<string> {
        "set a 1",
        "add a 2",
        "mul a a",
        "mod a 5",
        "snd a",
        "set a 0",
        "rcv a",
        "jgz a -1",
        "set a 1",
        "jgz a -2",   
    };
    var expected = 4;
    var testresult = Solve(testinput);
    if (testresult != expected) result = false;
    System.Diagnostics.Debug.Assert(expected == testresult, $"Expected {expected} but got {testresult}.");
    return result;
}