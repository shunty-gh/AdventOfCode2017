/// Advent of code 2017, day 1, part 1 in Node. Because, why not...
const fs = require('fs');

function getInput(day, part) {
    // Add leading zero, if necessary, to day number
    let dn = ("0" + day);
    dn = dn.substr(dn.length - 2);
    let fname = `./aoc2017-day${dn}-part${part}.txt`;
    return fs.readFile(fname, "utf-8", (err, data) => {
        inputLoaded(data);
    });
}

function inputLoaded(input) {
    let sum = 0;
    let inlen = input.length;
    for (let index = 0; index < inlen; index++) {
        let nextindex = index + 1;
        if (nextindex >= inlen) nextindex -= inlen;
        let ch = input[index];
        let nextch = input[nextindex];
        if (ch === nextch) {
            sum += parseInt(ch, 10);
        }
    }
    console.log("Result is: " + sum);
}

getInput(1, 1);