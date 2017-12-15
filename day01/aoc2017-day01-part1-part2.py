# Advent of code 2017
# Day 1, parts 1 & 2. In Python no less.

f = open("./aoc2017-day01-part1.txt", "r")
input = f.read().strip()
f.close()

inlen = len(input)
step = 1
result = sum(int(ch) for index, ch in enumerate(input) if ch == input[(index + step) % inlen])

print("Day 1, part 1 is: " + str(result))

step = int(inlen / 2)
result = sum(int(ch) for index, ch in enumerate(input) if ch == input[(index + step) % inlen])

print("Day 1, part 2 is: " + str(result))
