<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
    // Advent of code 2017
    // Day 14

    //var input = "flqrgnkx";  // Test input => 8108 bits set, 1242 regions
    var input = "ffayrhll";
    var result = Solve(input);
    Console.WriteLine($"Set bits: {result.Item1}");
    Console.WriteLine($"Groups: {result.Item2}");
}

public Tuple<int, int> Solve(string input)
{
    var grid = new int[128, 128];
    
    var setcount = 0;
    // Fill the grid and count the number of set bits
    for (var index = 0; index < 128; index++)
    {
        var hash = GetHash($"{input}-{index}");
        var row = HashToIntList(hash);
        for (var y = 0; y < 128; y++)
        {
            grid[index, y] = row[y];
        }
        setcount += row.Count(i => i == 1); // Part 1
    }

    // Walk the grid
    var visited = new HashSet<Point>();
    var groupcount = 0;
    for (var x = 0; x < 128; x++)
    {
        for (var y = 0; y < 128; y++)
        {
            if (grid[x, y] == 0)
                continue;

            var current = new Point(x, y);
            if (!visited.Contains(current))
            {
                groupcount++;
                var tovisit = new Queue<Point>();
                tovisit.Enqueue(current);
                visited.Add(current);
                while (tovisit.Count > 0)
                {
                    var p = tovisit.Dequeue();
                    foreach (var n in GetNeighbours(p, grid))
                    {
                        // Check if the neighbour is set
                        if ((grid[n.X, n.Y] == 1) && (!visited.Contains(n)))
                        {
                            visited.Add(n);
                            tovisit.Enqueue(n);
                        }
                    }
                }
            }
        }
    }
    return new Tuple<int, int>(setcount, groupcount);
}

public IEnumerable<Point> GetNeighbours(Point p, int[,] grid)
{
    var result = new List<Point>();
    if (p.X > 0)
    {
        result.Add(new Point(p.X - 1, p.Y));
    }
    if (p.X < grid.GetUpperBound(0))
    {
        result.Add(new Point(p.X + 1, p.Y));
    }
    if (p.Y > 0)
    {
        result.Add(new Point(p.X, p.Y - 1));
    }
    if (p.Y < grid.GetUpperBound(1))
    {
        result.Add(new Point(p.X, p.Y + 1));
    }
    return result;
}

public IList<int> HashToIntList(string hash)
{
    var result = new List<int>();
    foreach (var ch in hash)
    {
        var bits = Convert.ToString(Convert.ToByte($"0x{ch}", 16), 2)
            .PadLeft(4, '0')
            .ToCharArray()
            .Select(c => c == '1' ? 1 : 0);
        result.AddRange(bits);        
    }
    return result;
}

public string GetHash(string input)
{
    // From AoC Day 10
    var nums = Encoding.ASCII.GetBytes(input).ToList();
    nums.AddRange(new byte[] { 17, 31, 73, 47, 23 });
    var list = Enumerable.Range(0, 256).Select(x => x).ToList();
    var listlen = 256;
    var skipsize = 0;
    var current = 0;

    for (var round = 0; round < 64; round++)
    {
        foreach (var num in nums)
        {
            // Reverse section of num elements
            for (var index = 0; index < (num / 2); index++)
            {
                int a = (current + index) % listlen;
                int b = (current + num - index - 1) % listlen;
                var tmp = list[a];
                list[a] = list[b];
                list[b] = tmp;                
            }
            current = (current + num + skipsize) % listlen;
            skipsize++;
        }
    }

    var blocks = new int[16];
    for (var blockindex = 0; blockindex < 16; blockindex++)
    {
        blocks[blockindex] = list.Skip(blockindex * 16).Take(16).Aggregate(0, (h, b) => h ^= b);
    }
    // to hex string
    var result = blocks.Aggregate("", (s, b) => s += b.ToString("x2"));
    return result;
}
