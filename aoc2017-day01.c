#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/// Advent of code 2017
/// Day 1 part 1 in C

// Build with $> cl aoc2017-day01.c
// or $> gcc aoc2017-day01.c -o aoc2017-day01
// etc etc

// ReadFile pinched from https://stackoverflow.com/a/3464656
char* ReadFile(char *filename) {
    char *buffer = NULL;
    int str_size, read_size;
    FILE *src = fopen(filename, "r");

    if (src) {
        fseek(src, 0, SEEK_END);
        str_size = ftell(src);
        rewind(src);

        // Allocate buffer and read it in in one go and null terminate it
        buffer = (char*)malloc(sizeof(char) * (str_size + 1));
        read_size = fread(buffer, sizeof(char), str_size, src);
        buffer[str_size] = '\0';

        if (str_size != read_size) {  // It didn't work, clean up
            free(buffer);
            buffer = NULL;
        }

        fclose(src);
    }
    return buffer;
}

int main() {
    char* fname = "./aoc2017-day01-part1.txt";
    char* input = ReadFile(fname);

    if (input) {
        //printf("Input is %d characters\r\n", strlen(input));
        //puts(input);

        int inlen = strlen(input);
        int sum1 = 0, sum2 = 0;
        int step1 = 1, step2 = inlen / 2;
        int nextindex;

        // Part 1 - sum if next character matches
        for (int index = 0; index < inlen; index++) {
            nextindex = index + step1;
            if (nextindex >= inlen)
                nextindex -= inlen;

            if (input[index] == input[nextindex]) {
                sum1 += input[index] - '0';
            }
        }

        // Part 2 - sum if next len/2 character matches
        for (int index = 0; index < inlen; index++) {
            nextindex = index + step2;
            if (nextindex >= inlen)
                nextindex -= inlen;

            if (input[index] == input[nextindex]) {
                sum2 += input[index] - '0';
            }
        }

        free(input);
        printf("Part 1 result is %d\r\n", sum1);
        printf("Part 2 result is %d\r\n", sum2);
    }
    else {
        printf("Unable to load input");
    }

    return 0;
}
