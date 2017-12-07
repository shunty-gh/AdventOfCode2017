<Query Kind="Program" />

void Main()
{
    // Advent of code 2017
    // Day 1, part 1 & 2
    int day = 1;
    int part = 2;

    var input = ReadTextForDay(day, part);
    var result1 = Solve(input, 1);
    var result2 = Solve(input, input.Length / 2);
    Console.WriteLine($"Result, part 1: {result1}");
    Console.WriteLine($"Result, part 2: {result2}");
}

public int Solve(string input, int step)
{
    var sum = 0;
    var inputlength = input.Length;
    for (var index = 0; index < inputlength; index++)
    {
        sum += (input[index] == input[(index + step) % inputlength]) ? (int)(input[index] - '0') : 0;
    }
    return sum;
}

public string ReadTextForDay(int dayNumber, int part)
{
    var fname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{dayNumber:D2}-part{part:D1}.txt");
    var result = File.ReadAllText(fname, Encoding.UTF8);
    return result;
}
