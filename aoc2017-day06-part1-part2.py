# Advent of code 2017
# Day 6, parts 1 & 2

f = open("./aoc2017-day06-part1.txt", "r")
input = f.readlines()
f.close()

nums = [int(i) for i in input[0].strip().split("\t")]
#nums = [0,2,7,0]
numslen = len(nums)
round = 0
snapshots = dict()

#print(nums)
#print(numslen)

while True:
    snapshot = "_".join(str(n) for n in nums)
    #print(snapshot)

    if snapshot in snapshots:
        firstoccurrence = snapshots[snapshot]
        break
    snapshots[snapshot] = round

    elmax = max(nums)
    nextindex = nums.index(elmax)
    nums[nextindex] = 0
    while elmax > 0:
        nextindex = (nextindex + 1) % numslen
        nums[nextindex] += 1
        elmax -= 1
    round += 1

print("Total rounds:", round, " Rotations:", round - firstoccurrence)
