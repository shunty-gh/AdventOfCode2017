<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 21, parts 1 & 2
    int day = 21;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);
    var result = Solve(input, 5);
    Console.WriteLine($"Part 1, after 5 iterations: {result}");
    result = Solve(input, 18);
    Console.WriteLine($"Part 2, after 18 iterations: {result}");
}

public int Solve(IEnumerable<string> input, int iterations)
{
    var rules = BuildRules(input);
    var grid = ".#./..#/###".Split('/');
    while (iterations-- > 0)
    {
        grid = Enhance(grid, rules);
    }

    return grid.Select(s => s.ToCharArray().Count(c => c == '#')).Sum();
}

public Dictionary<string, string[]> BuildRules(IEnumerable<string>input)
{
    Func<string[], string[]> flip = src => src.Reverse().ToArray();

    Func<string[], string[]> rotate = src =>
        Enumerable.Range(0, src.Length)
            .Select(i => string.Join("", src.Reverse().Select(s => s[i])))
            .ToArray();

    var result = new Dictionary<string, string[]>();
    foreach (var rule in input)
    {
        var splits = rule.Split(new[] { " => "}, StringSplitOptions.RemoveEmptyEntries);
        var pattern = splits[0].Split('/');
        var ruleresult = splits[1].Split('/');
        
        for (var rots = 0; rots < 4; rots++)
        {
            result[string.Join("", pattern)] = ruleresult;
            result[string.Join("", flip(pattern))] = ruleresult;
            pattern = rotate(pattern);
        }
    }
    return result;
}

public string[] Enhance(string[] grid, Dictionary<string, string[]> rules)
{
    // Partition grid into 2x2 or 3x3 blocks depending on grid size
    // Expand 2x2 => 3x3 and 3x3 => 4x4
    var gl = grid.Length;
    var bw = gl % 2 == 0 ? 2 : 3;
    var bc = gl / bw;
    var result = new string[gl / bw * (bw + 1)];

    for (var y = 0; y < bc; y++)
    {
        for (var x = 0; x < bc; x++)
        {
            var key = string.Join("", Enumerable.Range(0, bw)
                .Select(i => grid[(y * bw) + i].Substring(x * bw, bw)));
                
            var transform = rules[key];

            for (var index = 0; index < bw + 1; index++)
            {
                result[(y * (bw + 1)) + index] += transform[index];
            }
        }
    }
    return result;
}
