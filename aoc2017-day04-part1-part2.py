# Advent of code 2017
# Day 4, parts 1 and 2

def isbad(srcwords):
    result = 0
    words = sorted(srcwords)
    for index in range(len(words) - 1):
        if words[index] == words[index + 1]:
            return True
    return False

f = open("./aoc2017-day04-part1.txt", "r")
input = f.readlines()
f.close()

result = 0
for line in input:
    badline = isbad(line.strip().split(' '))
    if badline == False:
        result += 1

print("Part 1:", result)

result = 0
for line in input:
    words = [''.join(sorted(w)) for w in line.strip().split(' ')]
    badline = isbad(words)
    if badline == False:
        result += 1

print("Part 2:", result)
