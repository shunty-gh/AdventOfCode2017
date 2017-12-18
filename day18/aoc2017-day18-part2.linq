<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
</Query>

void Main()
{
    // Advent of code 2017
    // Day 18, part 2
    int day = 18;

    var inputname = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $"aoc2017-day{day:D2}.txt");
    var input = File.ReadAllLines(inputname, Encoding.UTF8);
    //var input = new List<string> { "snd 1", "snd 2", "snd p", "rcv a", "rcv b", "rcv c", "rcv d" }; // test input => 3
    var result = Solve(input);
    Console.WriteLine($"Result, part 2: {result}");
}

public Int64 GetInstructionValue(IDictionary<string, Int64> registers, string keyOrValue)
{
    Int64 result;
    if (!Int64.TryParse(keyOrValue, out result))
    {
        if (!registers.ContainsKey(keyOrValue))
        {
            registers[keyOrValue] = 0;
        }
        result = registers[keyOrValue];
    }
    return result;
}

public static object locker = new Object();
public Task StartProgram(IList<string[]> instructions, Dictionary<string, Int64> registers, Queue<Int64> sendQueue, Queue<Int64> receiveQueue, ProgramTaskState state, Func<bool> deadlockCheck)
{
    var result = Task.Factory.StartNew(() =>
    {
        var instructionIndex = 0;
        while (instructionIndex >= 0 && instructionIndex < instructions.Count && !deadlockCheck())
        {
            var instruction = instructions[instructionIndex];
            var regx = instruction[1];
            int skip = 1;
            switch (instruction[0])
            {
                case "snd":
                    lock (locker)
                    {
                        sendQueue.Enqueue(GetInstructionValue(registers, regx));
                        state.SendCount += 1;
                    }
                    break;
                case "set":
                    registers[regx] = GetInstructionValue(registers, instruction[2]);
                    break;
                case "add":
                    registers[regx] += GetInstructionValue(registers, instruction[2]);
                    break;
                case "mul":
                    registers[regx] *= GetInstructionValue(registers, instruction[2]);
                    break;
                case "mod":
                    registers[regx] %= GetInstructionValue(registers, instruction[2]);
                    break;
                case "rcv":
                    lock (locker)
                    {
                        if (receiveQueue.Count > 0)
                        {
                            registers[regx] = receiveQueue.Dequeue();
                            state.WaitCount = 0;
                        }
                        else
                        {
                            state.WaitCount += 1;
                            skip = 0;
                        }
                    }
                    break;
                case "jgz":
                    skip = GetInstructionValue(registers, regx) > 0 ? (int)GetInstructionValue(registers, instruction[2]) : 1;
                    break;
                default:
                    throw new Exception($"Unknown instruction: {instruction[0]}");
            }

            instructionIndex += skip;
        }
    });
    return result;
}

public class ProgramTaskState
{
    public Int64 WaitCount;
    public Int64 SendCount;
}

public Int64 Solve(IEnumerable<string> input)
{
    var instructions = input.Select(s => s.Split(' ')).ToList().AsReadOnly();

    var registers0 = new Dictionary<string, Int64> { { "p", 0 } };
    var registers1 = new Dictionary<string, Int64> { { "p", 1 } };
    var queue0 = new Queue<Int64>();
    var queue1 = new Queue<Int64>();
    var state0 = new ProgramTaskState();
    var state1 = new ProgramTaskState();

    Func<bool> deadlockCheck = () =>
    {
        lock (locker)
        {
            var result = (state0.WaitCount > 2 && state1.WaitCount > 2);
            if (result)
                Console.WriteLine($"Deadlock! P0: {{ Wait = {state0.WaitCount}, Sends = {state0.SendCount} }} P1: {{ Wait = {state1.WaitCount}, Sends = {state1.SendCount} }}");
            return result;
        };
    };

    Task.WaitAll(new [] { 
        StartProgram(instructions, registers0, queue1, queue0, state0, deadlockCheck), 
        StartProgram(instructions, registers1, queue0, queue1, state1, deadlockCheck),
    });
    return state1.SendCount;
}
