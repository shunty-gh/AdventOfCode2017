""" Advent of Code 2017
    Day 19, parts 1 & 2
    https://adventofcode.com/2017/day/19
"""
from typing import Sequence, Tuple

def solve(instructions: Sequence[str]) -> Tuple[str, int]:
    """Follow the input map, collect letters and count number of steps"""

    north, south, west, east = (0, -1), (0, 1), (-1, 0), (1, 0)
    rowmax = len(instructions) - 1
    rowindex = 0
    colindex = instructions[rowindex].index('|')
    direction = south
    stepcount = 0
    result = ""

    while (rowindex <= rowmax and colindex >= 0
           and colindex < len(instructions[rowindex])):

        current = instructions[rowindex][colindex]

        if current == ' ':
            break
        elif current.isalpha():
            result += current
        elif current == '+':
            if direction == north or direction == south:
                if colindex > 0 and instructions[rowindex][colindex - 1] != ' ':
                    direction = west
                else:
                    direction = east
            else:
                if instructions[rowindex - 1][colindex] != ' ':
                    direction = north
                else:
                    direction = south
        # if we assume no invalid input chars then we don't need this check
        #elif current == '|' or current == '-':
        #    pass

        stepcount += 1
        colindex += direction[0]
        rowindex += direction[1]

    return (result, stepcount)

def main():
    """AoC 2017, day 19"""

    fhandle = open("./aoc2017-day19.txt", "r")
    instructions = fhandle.readlines()
    fhandle.close()

    pt1, pt2 = solve(instructions)
    print("Result, part 1:", pt1)
    print("Result, part 2:", pt2)


if __name__ == "__main__":
    main()
