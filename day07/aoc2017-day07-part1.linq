<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 7, part1
    int day = 7;
    int part = 1;

    if (!RunTests())
        return;

    var input = ReadLinesForDay(day, part);
    var result = Solve(input);
    Console.WriteLine($"Result: {result}");
}

public string Solve(IEnumerable<string> input)
{
    var towers = new Dictionary<string, Tower>();
    var result = "";
    foreach (var line in input)
    {
        var tower = ProcessLine(line);
        towers.Add(tower.TowerName, tower);
    }

    //towers.Dump();
    foreach (var kvp in towers)
    {
        var tower = kvp.Value;
        var above = tower.Towers;
        if (!string.IsNullOrWhiteSpace(above))
        {
            var split = above.Trim().Split(',');
            foreach (var tname in split)
            {
                var subtower = towers[tname.Trim()];
                subtower.Below = tower;
            }
        }
    }
    
    result = towers.First(t => t.Value.Below == null).Key;
    return result;
}

public class Tower
{
    public string TowerName { get; set; }   
    public int Weight { get; set; }
    public string Towers { get; set; }    
    public Tower Below { get; set; }
}

static string pattern = @"(?<tower>\w*)\s\((?<weight>\d+)\)(\s*->\s*(?<towers>.*))*";
static Regex re = new Regex(pattern);
public Tower ProcessLine(string line)
{
    var match = re.Match(line);
    //match.Dump();
    var tower = new Tower
    {
        TowerName = match.Groups["tower"].Value,
        Weight = int.Parse(match.Groups["weight"].Value),
        Towers = match.Groups["towers"]?.Value ?? "",
    };
    return tower;
}

public bool RunTests()
{
    var result = true;
    var fname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day07-part1-test.txt");
    var testinput = File.ReadAllLines(fname, Encoding.UTF8);

    var expected = "tknk";
    var testresult = Solve(testinput);
    if (testresult != expected) result = false;
    System.Diagnostics.Debug.Assert(expected == testresult, $"Expected {expected} but got {testresult}.");
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