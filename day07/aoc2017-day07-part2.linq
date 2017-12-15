<Query Kind="Program">
  <Connection>
    <ID>90b8ac7a-faac-4783-aecf-d1f220ab5b73</ID>
    <Persist>true</Persist>
    <Server>(localdb)\MSSQLLocalDb</Server>
    <Database>apollo-payroll</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
    // Advent of code 2017
    // Day 7, part 2
    int day = 7;
    int part = 1;

    if (!RunTests())
        return;

    var input = ReadLinesForDay(day, part);
    var result = Solve(input);
    Console.WriteLine($"Result: {result}");
}

public int Solve(IEnumerable<string> input)
{
    var towers = new Dictionary<string, Tower>();
    foreach (var line in input)
    {
        var tower = ProcessLine(line);
        towers.Add(tower.TowerName, tower);
    }

    //towers.Dump();
    // Link all the towers together
    foreach (var kvp in towers)
    {
        var tower = kvp.Value;
        var tnames = tower.SubTowerNames;
        if (!string.IsNullOrWhiteSpace(tnames))
        {
            var split = tnames.Trim().Split(',');
            foreach (var tname in split)
            {
                var subtower = towers[tname.Trim()];
                tower.AddSubTower(subtower);
            }
        }
    }

    var basetower = towers.First(t => t.Value.Parent == null).Value;
    //basetower.Dump("Base");
    // Iterate through the towers looking for the sub-tower that is 
    // not balanced and recurse through until we get to a point where
    // all sub-towers of the current tower are balanced.
    var current = basetower;
    while(true)
    {
        var unbal = current.SubTowers.FirstOrDefault(t => !t.IsBalanced);
        if (unbal == null)
            break;
        current = unbal;
    }
    //current.Dump();
    // At this point the current tower has one sub tower with a bad weight
    // as each of the current tower's sub-towers will be balanced on its own 
    // but one sub-tower total weight will be different to the others.
    // ie curent tower is unbalanced but its sub-towers are individually balanced
    var towerweights = current.SubTowers.GroupBy(t => t.TotalWeight);
    //towerweights.Dump();
    // One grouped item in the towerweights grouping will have only one entry - the 
    // bad tower/weight - and the other grouped item will have > 1 entry - the 
    // good towers/weights
    var badtower = towerweights.Where(g => g.Count() == 1).Select(g => g.First()).First(); // The bad tower
    var goodtotal = towerweights.Where(g => g.Count() > 1).Select(g => g.Key).First(); // The correct weight
    //badtower.Dump();
    //goodtotal.Dump();
    var diff = badtower.TotalWeight - goodtotal;
    var corrected = badtower.TowerWeight - diff;

    return corrected;
}

public class Tower
{
    private int _subtowerweight = -1;
    private List<Tower> _subtowers = new List<Tower>();
    
    public string TowerName { get; set; }
    public int TowerWeight { get; set; }
    public int TotalWeight { get { return TowerWeight + SubTowerWeight; } }
    public string SubTowerNames { get; set; }
    public IEnumerable<Tower> SubTowers { get { return _subtowers; } }
    public Tower Parent { get; set; }
    
    public void AddSubTower(Tower tower)
    {
        tower.Parent = this;
        _subtowers.Add(tower);
    }

    public int SubTowerWeight
    {
        get
        {
            if (_subtowers.Count == 0)
                return 0;
                
            // Calculate it the first time and store it
            if (_subtowerweight < 0)
            {
                var result = 0;
                foreach (var t in _subtowers)
                {
                    result += t.TotalWeight;
                }
                _subtowerweight = result;
            }
            return _subtowerweight;
        }
    }
    
    public bool IsBalanced
    {
        get
        {
            if (_subtowers.Count == 0)
                return true;
            int w1 = _subtowers[0].TotalWeight;
            foreach (var t in _subtowers.Skip(1))
            {
                if (t.TotalWeight != w1)
                    return false;
            }
            return true;
        }
    }
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
        TowerWeight = int.Parse(match.Groups["weight"].Value),
        SubTowerNames = match.Groups["towers"]?.Value ?? "",
    };
    return tower;
}

public bool RunTests()
{
    var result = true;
    var fname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day07-part1-test.txt");
    var testinput = File.ReadAllLines(fname, Encoding.UTF8);

    var expected = 60;
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