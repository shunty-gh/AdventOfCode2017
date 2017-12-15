# Advent of Code 2017
# Day 14

input = "flqrgnkx"

def generate_hash(source):
    return source + "fred"

hash = generate_hash(input + "-0")
print("Hash is {}", hash)
