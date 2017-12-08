use std::io::prelude::*;
use std::io::BufReader;
use std::fs::File;
use std::path::Path;
use std::collections::HashMap;

/// Advent of Code 2017
/// Day 6, parts 1 and 2

// With links to the Rust book 'cos I'm trying to learn Rust...

// compile with, for example:
// $> rustc -g --out-dir ./bin aoc2017-day06.rs

fn lines_from_file<P>(filename: P) -> Vec<String>
where
    P: AsRef<Path>,
{
    let file = File::open(filename).expect("no such file");
    let buf = BufReader::new(file);
    buf.lines()
        .map(|l| l.expect("Could not parse line"))
        .collect()
}

fn main() {
    let input = lines_from_file("./aoc2017-day06-part1.txt");
    let line = &input[0]; // Only one line today

    // https://doc.rust-lang.org/book/second-edition/ch08-01-vectors.html
    let mut nums: Vec<i32> = line.split("\t")
        .map(|s| s.parse().unwrap())
        .collect();

    let len = nums.len();
    let mut rounds = 0;

    // https://doc.rust-lang.org/book/second-edition/ch08-03-hash-maps.html
    let mut snapshots = HashMap::new();

    while !snapshots.contains_key(&nums) {
        snapshots.insert(nums.clone(), rounds);

        // Find the max value and the index of it
        // https://doc.rust-lang.org/book/second-edition/ch06-03-if-let.html

        // let (idx, &v) = nums.iter()
        //     .enumerate()
        //     .max_by_key(|&(i, val)| (val, -(i as isize)))
        //     .unwrap();
        // or use 'if let...':
        if let Some((idx, &v)) = nums.iter()
            .enumerate()
            // the max_by_key function parameter needs to return an orderable 'thing'
            // on which to determine the max. Here we use a tuple of 2 integers.
            // The first integer is the value itself - but there may be more than one
            // element with the same value and the last one encountered would be
            // returned - but we want the first one. Therefore we add the -ve of the
            // current index as the second element of the tuple, and, hence, the second
            // part of the ordering/comparison. This will give us the first occurrence
            // of the max value (eg element with value 11 at indexes 5 and 13; Normal
            // max_by... would return index 13 but if we add the -ve index to the compare
            // then -5 is before -13 and so we get the right value.
            // For some reason it has taken me ages to "get" this one line.
            .max_by_key(|&(i, val)| (val, -(i as isize)))  {

            nums[idx] = 0;

            // Reallocate
            // This works:
            // let mut mval = v;
            // let mut nextindex = idx;
            // while mval > 0 {
            //     nextindex = (nextindex + 1) % len;
            //     nums[nextindex] += 1;
            //     mval -= 1;
            // }
            //...but this is better:
            for index in 0..(v as usize) {
                nums[(idx + index + 1) % len] += 1;
            }
        }
        rounds += 1;
    }

    //let repeatlen = rounds - snapshots.get(&nums).unwrap();
    let repeatlen = rounds - snapshots[&nums];
    //println!("Nums {:?}", nums);
    println!("Part 1: Rounds = {}", rounds);
    println!("Part 2: Repeat length = {}", repeatlen);
}