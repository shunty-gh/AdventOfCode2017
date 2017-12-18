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

public struct Instruction
{
    public string Action;
    public char OpReg;
    public Int64 OpValue;
    public Int64 Amount;
}

public void CheckRegister(IDictionary<char, Int64> registers, char key)
{
    if ((key != '_') && !registers.ContainsKey(key))
        registers[key] = 0;
}

public Instruction ParseInstruction(string[] src, IDictionary<char, Int64> registers)
{
    var tmp = src[1];
    char regX = ((tmp.Length == 1) && Char.IsLetter(tmp[0])) ? tmp[0] : '_';
    CheckRegister(registers, regX);
    Int64 regXvalue = regX == '_' ? Int64.Parse(tmp) : registers[regX];
    
    tmp = (src.Count() > 2) ? src[2] : "0";
    char regY = ((tmp.Length == 1) && Char.IsLetter(tmp[0])) ? tmp[0] : '_';
    CheckRegister(registers, regY);
    Int64 regYvalue = regY == '_' ? Int64.Parse(tmp) : registers[regY];

    return new Instruction
    {
        Action = src[0],
        OpReg = regX,
        OpValue = regXvalue,
        Amount = regYvalue 
    };
}

public static object locker = new Object();
public int ProcessInstruction(int programId, int currentInstructionIndex, Instruction instruction, IDictionary<char, Int64> registers, Queue<Int64> sendQueue, Queue<Int64> rcvQueue)
{
    int result;
    switch (instruction.Action)
    {
        case "snd":
            lock (locker)
            {
                sendQueue.Enqueue(instruction.OpValue);
                result = currentInstructionIndex + 1;
            }
            break;
        case "set":
            registers[instruction.OpReg] = instruction.Amount;
            result = currentInstructionIndex + 1;
            break;
        case "add":
            registers[instruction.OpReg] += instruction.Amount;
            result = currentInstructionIndex + 1;
            break;
        case "mul":
            registers[instruction.OpReg] *= instruction.Amount;
            result = currentInstructionIndex + 1;
            break;
        case "mod":
            registers[instruction.OpReg] %= instruction.Amount;
            result = currentInstructionIndex + 1;
            break;
        case "rcv":
            result = currentInstructionIndex;
            if (rcvQueue.Count > 0)
            {
                lock (locker)
                {
                    registers[instruction.OpReg] = rcvQueue.Dequeue();
                    result += 1;
                }
            }
            break;
        case "jgz":
            result = currentInstructionIndex + (int)(instruction.OpValue > 0 ? instruction.Amount : 1);
            break;
        default:
            throw new Exception($"Unknown instruction: {instruction.Action}");
    }
    return result;
}

public Task StartProgram(int programId, IList<string[]> instructions, Dictionary<char, Int64> registers, List<Queue<Int64>> queues, IList<ProgramTaskState> states)
{
    var result = Task.Factory.StartNew(() =>
    {
        var sndq = programId == 0 ? queues[1] : queues[0];
        var rcvq = programId == 0 ? queues[0] : queues[1];
        var state = states[programId];
        var instructionIndex = 0;
        while (true)
        {
            var instruction = ParseInstruction(instructions[instructionIndex], registers);
            var nextindex = ProcessInstruction(programId, instructionIndex, instruction, registers, sndq, rcvq);
            state.WaitCount = (nextindex == instructionIndex) ? state.WaitCount + 1 : 0; 

            if (instruction.Action == "snd")
            {
                state.SendCount += 1;
            }

            if (instructionIndex < 0 || instructionIndex >= instructions.Count)
            {
                state.WaitCount = 0;
                break; // Finished if we jump out of the instruction list
            }

            instructionIndex = nextindex;
            
            // Deadlock check
            lock (locker)
            {
                if (states[0].WaitCount > 2 && states[1].WaitCount > 2)
                {
                    // Both programs are waiting, and have been for more than two rounds each
                    // therefore, deadlock
                    Console.WriteLine($"P{programId} deadlock! P0: {{ Wait = {states[0].WaitCount}, Sends = {states[0].SendCount} }} P1: {{ Wait = {states[1].WaitCount}, Sends = {states[1].SendCount} }}");
                    break;
                }
            }
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
    var instructions = input.Select(s => s.Split(' ')).ToList();

    var registers = new List<Dictionary<char, Int64>> {
        new Dictionary<char, Int64> { { 'p', 0 } }, 
        new Dictionary<char, Int64> { { 'p', 1 } },
    };
    var queues = new List<Queue<Int64>> {
        new Queue<Int64>(),
        new Queue<Int64>(),
    };
    var states = new List<ProgramTaskState> { 
        new ProgramTaskState(), 
        new ProgramTaskState() 
    };
    
    var p0 = StartProgram(0, instructions, registers[0], queues, states);
    var p1 = StartProgram(1, instructions, registers[1], queues, states);

    Task.WaitAll(new [] { p0, p1 });
    return states[1].SendCount;
}
