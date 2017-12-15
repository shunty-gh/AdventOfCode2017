<Query Kind="Program">
  <Namespace>System</Namespace>
</Query>

void Main()
{
    // Advent of code 2017
    // Day 15

    //var input = new Tuple<int, int>(65, 8921); // Test input => part1: 588; part2: 309
    var input = new Tuple<int, int>(703, 516);
    var result = Solve(input);
    Console.WriteLine($"Result, part 1: {result.Item1}");
    Console.WriteLine($"Result, part 2: {result.Item2}");
}

public Tuple<int, int> Solve(Tuple<int, int> input)
{
    Int64 factorA = 16807;
    Int64 factorB = 48271;
    int mask = 65535;
    int part1 = 0;
    
    int gena = input.Item1;
    int genb = input.Item2;
    var foundA = new List<int>();
    var foundB = new List<int>();
    int maxrounds = 40_000_000;
    int maxconsider = 5_000_000;
    int rounds = maxrounds;
    while (rounds-- > 0 || foundA.Count < maxconsider || foundB.Count < maxconsider)
    {
        gena = (int)(gena * factorA % int.MaxValue);
        genb = (int)(genb * factorB % int.MaxValue);
        
        if (rounds < maxrounds)
            part1 += (gena & mask) == (genb & mask) ? 1 : 0;

        if ((foundA.Count < maxconsider) && (gena % 4 == 0))
            foundA.Add(gena & mask);
        if ((foundB.Count < maxconsider) && (genb % 8 == 0))
            foundB.Add(genb & mask);
    }
    
    var part2 = foundA.Zip(foundB, (a, b) => a == b ? 1 : 0).Sum(s => s);
    return new Tuple<int, int>(part1, part2);
}
    