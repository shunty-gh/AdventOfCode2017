use std::io::prelude::*;
use std::fmt;
use std::io::BufReader;
use std::fs::File;
use std::path::Path;
use std::env;

/// Advent of Code 2017
/// Day 12

// compile with, for example:
//     $> rustc -g --out-dir ./bin aoc2017-day12.rs
// or (providing the 'path' is set correctly in Cargo.toml):
//     $> cargo build

fn main() {
    let args: Vec<String> = env::args().collect();
    let inputname: &str;
    if args.len() > 1 {
        inputname = &args[1];
    } else {
        inputname = "./aoc2017-day12.txt";
    }

    let input = lines_from_file(inputname);

    let programs: Vec<Program> = input.iter()
        .map(|line| {
            let splits: Vec<&str> = line.split(" <-> ").collect();

            Program {
                id: splits[0].to_string().parse::<usize>().unwrap(),
                connections: splits[1].to_string()
                    .split(", ")
                    .map(|s| s.parse::<usize>().unwrap())
                    .collect(),
            }
        })
        .collect();
    //println!("Programs: {:?}", programs);

    let mut visited: Vec<usize> = vec![];
    let mut group_count = 0;
    let mut group_zero_count = 0;

    for program in &programs {
        let key = &program.id;
        if visited.contains(key) {
            continue;
        }

        group_count += 1;
        let mut to_visit: Vec<usize> = vec![*key];
        while to_visit.len() > 0 {
            let current = to_visit.pop().unwrap();
            if !visited.contains(&current) {
                visited.push(current);

                for conn in &programs[current].connections {
                    if !visited.contains(conn) {
                        to_visit.push(*conn);
                    }
                }
            }
        }
        if *key == 0 {
            group_zero_count = visited.len();
        }
    }

    assert!(programs.len() == visited.len(), "Number visited should equal total number of programs");
    println!("Using {} as input", &inputname);
    println!("Num programs: {}", programs.len());
    println!("Programs connected to P0 (part 1): {}", group_zero_count);
    println!("Number of groups (part 2): {}", group_count);
}

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

struct Program {
    id: usize,
    connections: Vec<usize>,
}

impl fmt::Debug for Program {
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
        write!(f, "Prog id: {}; Connections: {:?}", self.id, self.connections)
    }
}
