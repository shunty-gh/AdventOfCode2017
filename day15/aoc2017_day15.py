""" Advent of Code 2017
    Day 15
"""

from threading import Thread

FACTOR_A = 16807
FACTOR_B = 48271
DIVMOD = 2147483647
MASK = 65535

def part1(init_a, init_b, results):
    gena = init_a
    genb = init_b
    result = 0
    for _ in range(40_000_000):
        gena = gena * FACTOR_A % DIVMOD
        genb = genb * FACTOR_B % DIVMOD
        if gena & MASK == genb & MASK:
            result += 1

    results.update({"Part 1": result})
    return result


def part2(init_a, init_b, results):
    gena = init_a
    genb = init_b
    compared = 0
    result = 0
    while compared < 5_000_000:
        while True:
            gena = gena * FACTOR_A % DIVMOD
            if gena % 4 == 0:
                break
        while True:
            genb = genb * FACTOR_B % DIVMOD
            if genb % 8 == 0:
                break

        compared += 1
        if (gena & MASK) == (genb & MASK):
            result += 1

    results.update({"Part 2": result})
    return result

def main():
    # Original input data
    gen_a_org = 703
    gen_b_org = 516

    results = {}
    pt1 = Thread(target=part1, args=(gen_a_org, gen_b_org, results))
    pt2 = Thread(target=part2, args=(gen_a_org, gen_b_org, results))
    pt1.start()
    pt2.start()

    pt1.join()
    pt2.join()

    for res in sorted(results.items(), key=lambda x: x[0]):
        print(res[0], res[1])


if __name__ == "__main__":
    main()
