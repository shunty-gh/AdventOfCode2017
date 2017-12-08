use std::io::prelude::*;
use std::fmt;
use std::io::BufReader;
use std::fs::File;
use std::path::Path;
use std::collections::HashMap;

/// Advent of Code 2017
/// Day 8, parts 1 and 2

// compile with, for example:
// $> rustc -g --out-dir ./bin aoc2017-day08.rs

fn main() {
    let input = lines_from_file("./aoc2017-day08-part1.txt");
    //let input = vec!["b inc 5 if a > 1", "a inc 1 if b < 5", "c dec -10 if a >= 1", "c inc -20 if c == 10"];
    let mut registers = HashMap::new();
    let mut largest = 0;

    for line in input {
        let splits: Vec<&str> = line.split(" ").collect();

        let rc = RegisterCondition {
            register: splits[4].to_string(),
            operator: splits[5].to_string(),
            amount: splits[6].to_string().parse().unwrap(),
        };

        let incdec = if splits[1].to_string() == "inc" { 1 } else { -1 };
        let ri = RegisterInstruction {
            register: splits[0].to_string(),
            inc_dec: incdec,
            amount: splits[2].to_string().parse().unwrap(),
        };

        if !registers.contains_key(&ri.register) {
            registers.insert(ri.register.clone(), 0);
        }
        if !registers.contains_key(&rc.register) {
            registers.insert(rc.register.clone(), 0);
        }

        let &mut condreg = registers.entry(rc.register.clone()).or_insert(0);
        let condamnt = rc.amount;
        let applyit = match rc.operator.as_ref() {
            "==" => if condreg == condamnt { true } else { false },
            "!=" => if condreg != condamnt { true } else { false },
            ">" => if condreg > condamnt { true } else { false },
            "<" => if condreg < condamnt { true } else { false },
            ">=" => if condreg >= condamnt { true } else { false },
            "<=" => if condreg <= condamnt { true } else { false },
            _ => false,
        };
        if applyit {
            let amnt = &ri.amount * &ri.inc_dec;
            let reg = registers.entry(ri.register.clone()).or_insert(0);
            *reg += amnt;
            if *reg > largest {
                largest = *reg;
            }
        }

        //println!("RC {:?}", rc);
        //println!("RI {:?}", ri);
    }

    let (key, result) = registers.iter().max_by_key(|&(_, x)| x).unwrap();
    //println!("Registers: {:?}", registers);
    println!("Largest at end (part 1): register '{}' with value {:?},", key, result);
    println!("Largest value in use (part 2): {:?}", largest);
}

struct RegisterInstruction {
    register: String,
    inc_dec: i32,
    amount: i32,
}

impl fmt::Debug for RegisterInstruction {
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
        write!(f, "Register: {}; Inc: {}; Amount: {}", self.register, self.inc_dec, self.amount)
    }
}

struct RegisterCondition {
    register: String,
    operator: String,
    amount: i32,
}

impl fmt::Debug for RegisterCondition {
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
        write!(f, "Register: {}; Op: {}; Amount: {}", self.register, self.operator, self.amount)
    }
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
