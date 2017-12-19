<Query Kind="Program">
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
    // Advent of code 2017
    // Day 19, parts 1 & 2
    int day = 19;

    if (!RunTests())
        return;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);
    var result = Solve(input);
    Console.WriteLine($"Result, part 1: {result.Item1}");
    Console.WriteLine($"Result, part 2: {result.Item2}");
}

public Tuple<string, int> Solve(IList<string> input)
{
    Point north = new Point(0, -1), south = new Point(0, 1), west = new Point(-1, 0), east = new Point(1, 0);
    var result = "";
    var rowindex = 0;
    var colindex = input[rowindex].IndexOf('|');
    var direction = south;
    var stepcount = 0;

    while (rowindex < input.Count && colindex >= 0 && colindex < input[rowindex].Length)
    {
        var current = input[rowindex][colindex];
        //Console.WriteLine($"Current: {current}");

        if (current == ' ') // reached the end
        {
            break;
        }   
        else if (Char.IsLetter(current))
        {
            result += current;
        }
        else if (current == '+') // change direction
        {
            if (direction == north || direction == south)
            {
                if ((colindex > 0) && (input[rowindex][colindex - 1] != ' '))
                    direction = west;
                else
                    direction = east;
            }
            else // direction = east || west
            {
                if (input[rowindex - 1][colindex] != ' ') // rowindex always > 0
                    direction = north;
                else
                    direction = south;
            }
        }
        else if (current != '|' && current != '-')
        {
            throw new Exception($"Unknown path character '{current}'");
        }

        stepcount++;
        colindex += direction.X;
        rowindex += direction.Y;
    }
    return new Tuple<string, int>(result, stepcount);
}

public bool RunTests()
{
    var result = true;
    var testinput = new List<string> {
        "    |           ",
        "    |  +--+     ",
        "    A  |  C     ",
        "F---|----E|--+  ",
        "    |  |  |  D  ",
        "    +B-+  +--+  ",
    };
    var expected = new Tuple<string, int>("ABCDEF", 38);
    var testresult = Solve(testinput);
    if ((testresult.Item1 != expected.Item1) && (testresult.Item2 != expected.Item2)) result = false;
    System.Diagnostics.Debug.Assert(expected.Item1 == testresult.Item1, $"Expected {expected.Item1} but got {testresult.Item1}.");
    System.Diagnostics.Debug.Assert(expected.Item2 == testresult.Item2, $"Expected {expected.Item2} but got {testresult.Item2}.");
    return result;
}