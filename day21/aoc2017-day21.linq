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

public Dictionary<string, string[]> BuildRules(IEnumerable<string>input)
{
    var result = new Dictionary<string, string[]>();
    foreach (var rule in input)
    {
        var splits = rule.Split(new[] { " => "}, StringSplitOptions.RemoveEmptyEntries);
        var pattern = splits[0].Split('/');
        var ruleresult = splits[1].Split('/');
        
        for (var rots = 0; rots < 4; rots++)
        {
            result[string.Join("", pattern)] = ruleresult;
            result[string.Join("", Flip(pattern, true))] = ruleresult;
            result[string.Join("", Flip(pattern, false))] = ruleresult;
            pattern = Rotate(pattern);
        }
    }
    return result;
}

public string[] Rotate(string[] input)
{
    // Rotate a grid clockwise
    // eg  a b c      g d a
    //     d e f  =>  h e b
    //     g h i      i f c
    
    return Enumerable.Range(0, input.Length)
        .Select(i => 
            string.Join("", input.Reverse().Select(s => s[i])))
        .ToArray();
}

public string[] Flip(string[] input, bool horiz)
{
    // Flip a grid along the horizontal or vertical axis
    return horiz 
        ? input.Reverse().ToArray()
        : input.Select(l => string.Join("", l.ToCharArray().Reverse().ToArray())).ToArray();
}

public int Solve(IEnumerable<string> input, int iterations)
{
    var rules = BuildRules(input);
    var grid = ".#./..#/###".Split('/');
    while (iterations > 0)
    {      
        var splits = SliceGrid(grid);
        var enhanced = splits.Select(g => rules[string.Join("", g)]).ToList();
        grid = Recombine(enhanced);

        iterations--;
    }
    
    return grid.Select(s => s.ToCharArray().Count(c => c == '#')).Sum();
}

public string[] Recombine(IList<string[]> grids)
{
    if (grids.Count == 1)
        return grids[0];
        
    var gcount = grids.Count;
    var blockwidth = grids[0].Length;
    var blocksperrow = (int)Math.Sqrt(gcount);
    var result = new string[blocksperrow * blockwidth];
    for (var y = 0; y < blocksperrow; y++)
    {        
        for (var x = 0; x < blockwidth; x++)
        {
            var s = "";
            for (var index = 0; index < blocksperrow; index++)
            {
                s += grids[y * blocksperrow + index][x];
            }
            result[(y * blockwidth) + x] = s;
        }
    }
    return result;
}

public IList<string[]> SliceGrid(string[] grid)
{
    var gwidth = grid.Length;
    var blockwidth = gwidth % 2 == 0 ? 2 : 3;
    var blocksperrow = gwidth / blockwidth;
    if (gwidth == blockwidth)
        return new List<string[]> { grid };
    
    var result = new List<string[]>();
    for (var y = 0; y < blocksperrow; y++)
    {
        for (var x = 0; x < blocksperrow; x++)
        {
            var block = new string[blockwidth];
            for (var index = 0; index < blockwidth; index++)
            {
                var row = grid[(y * blockwidth) + index].Substring(x * blockwidth, blockwidth);
                block[index] = row;
            }
            result.Add(block);
        }
    }
    return result;
}