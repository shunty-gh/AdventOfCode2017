<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 4, part1
    var fname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "aoc2017-day04-part1.txt");
    var input = File.ReadAllLines(fname, Encoding.UTF8);
    
    var result = Solve(input);
    Console.WriteLine($"Part 1 result is {result}");

    // Sort each part of the input
    var input2 = input.Select(l => l.Split(' '))
        .Select(l => string.Join(" ", l.Select(ll => string.Concat(ll.ToCharArray().OrderBy(c => c)))));
    
    result = Solve(input2);
    Console.WriteLine($"Part 2 result is {result}");
}

public int Solve(IEnumerable<string> input)
{
    var pattern = @"^(\w*\s+)*(?<dup>\w*)\s+(.*\s)*(\k<dup>)(\s+\w*)*$";
    var re = new Regex(pattern);

    var result = 0;
    foreach (var line in input)
    {
        if (!re.IsMatch(line))
            result++;
    }
    return result;
}
