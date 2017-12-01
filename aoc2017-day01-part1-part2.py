# Advent of code 2017
# Day 1, parts 1 & 2. In Python no less.

f = open("./aoc2017-day01-part1.txt", "r")
input = f.read()
f.close()

index = 0
sum = 0
inlen = len(input)
step = 1
for ch in input:
    nextindex = index + step
    if nextindex >= inlen:
        nextindex -= inlen
    if ch == input[nextindex]:
        sum += int(ch)

    index += 1

print("Day 1, part 1 is: " + str(sum))

f = open("./aoc2017-day01-part2.txt", "r")
input = f.read()
f.close()

index = 0
sum = 0
inlen = len(input)
step = int(inlen / 2)
for ch in input:
    nextindex = index + step
    if nextindex >= inlen:
        nextindex -= inlen
    if ch == input[nextindex]:
        sum += int(ch)

    index += 1

print("Day 1, part 2 is: " + str(sum))
