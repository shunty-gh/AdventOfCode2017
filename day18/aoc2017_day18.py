""" Advent of Code 2017
    Day 18, part 2
"""

from collections import defaultdict, deque

def get_register_value(registers, key_or_value):
    try:
        return int(key_or_value)
    except ValueError:
        return registers[key_or_value]


def part1(instructions):
    registers = defaultdict(int)
    lastsnd = 0
    result = 0
    index = 0
    while result == 0:
        action, regx, *params = instructions[index].strip().split(" ")
        skip = 1
        if action == "snd":
            lastsnd = get_register_value(registers, regx)
        elif action == "set":
            regy = params[0]
            registers[regx] = get_register_value(registers, regy)
        elif action == "add":
            regy = params[0]
            registers[regx] += get_register_value(registers, regy)
        elif action == "mul":
            regy = params[0]
            registers[regx] *= get_register_value(registers, regy)
        elif action == "mod":
            regy = params[0]
            registers[regx] %= get_register_value(registers, regy)
        elif action == "rcv":
            if get_register_value(registers, regx) > 0:
                result = lastsnd
        elif action == "jgz":
            regy = params[0]
            if get_register_value(registers, regx) > 0:
                skip = get_register_value(registers, regy)
        else:
            print("Unknown action", action)

        index += skip

    return result


def part2(instructions):
    registers0 = defaultdict(int)
    registers1 = defaultdict(int, {"p": 1})
    q0 = deque()
    q1 = deque()

    index0 = 0
    index1 = 0
    wait0 = 0
    wait1 = 0
    sendcount = 0
    while not (wait0 and wait1):
        skip = process_instruction(instructions[index0], registers0, q1, q0)
        if skip == 0:
            wait0 += 1
        index0 += skip

        skip = process_instruction(instructions[index1], registers1, q0, q1)
        if instructions[index1].startswith("snd"):
            sendcount += 1
        if skip == 0:
            wait1 += 1
        index1 += skip

    return sendcount


def process_instruction(instruction, registers, sendq, recvq):
    action, regx, *params = instruction.strip().split(" ")
    skip = 1
    if action == "snd":
        sendq.append(get_register_value(registers, regx))
    elif action == "set":
        regy = params[0]
        registers[regx] = get_register_value(registers, regy)
    elif action == "add":
        regy = params[0]
        registers[regx] += get_register_value(registers, regy)
    elif action == "mul":
        regy = params[0]
        registers[regx] *= get_register_value(registers, regy)
    elif action == "mod":
        regy = params[0]
        registers[regx] %= get_register_value(registers, regy)
    elif action == "rcv":
        if len(recvq) > 0:
            registers[regx] = recvq.popleft()
        else:
            skip = 0
    elif action == "jgz":
        regy = params[0]
        if get_register_value(registers, regx) > 0:
            skip = get_register_value(registers, regy)
    else:
        print("Unknown action", action)

    return skip


def main():
    f = open("./aoc2017-day18.txt", "r")
    instructions = f.readlines()
    f.close()

    pt1 = part1(instructions)
    print("Last sound (part 1):", pt1)
    pt2 = part2(instructions)
    print("P1 send count (part 2):", pt2)


if __name__ == "__main__":
    main()
