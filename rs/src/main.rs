use core::panicking::panic;
use std::collections::HashMap;
use std::fs;

fn main() {
    let numbers = [
        ("førti", 40),
        ("tjue", 20),
        ("femti", 50),
        ("tretten", 13),
        ("tretti", 30),
        ("tolv", 12),
        ("ti", 10),
        ("elleve", 11),
        ("fjorten", 14),
        ("femten", 15),
        ("seksten", 16),
        ("sytten", 17),
        ("atten", 18),
        ("nitten", 19),
        ("tjueen", 21),
        ("tjueto", 22),
        ("tjuetre", 23),
        ("tjuefire", 24),
        ("tjuefem", 25),
        ("tjueseks", 26),
        ("tjuesju", 27),
        ("tjueåtte", 28),
        ("tjueni", 29),
        ("trettien", 31),
        ("trettito", 32),
        ("trettitre", 33),
        ("trettifire", 34),
        ("trettifem", 35),
        ("trettiseks", 36),
        ("trettisju", 37),
        ("trettiåtte", 38),
        ("trettini", 39),
        ("førtien", 41),
        ("førtito", 42),
        ("førtitre", 43),
        ("førtifire", 44),
        ("førtifem", 45),
        ("førtiseks", 46),
        ("førtisju", 47),
        ("førtiåtte", 48),
        ("førtini", 49),
        ("en", 1),
        ("to", 2),
        ("tre", 3),
        ("fire", 4),
        ("fem", 5),
        ("seks", 6),
        ("sju", 7),
        ("åtte", 8),
        ("ni", 9),
    ];

    let mut tmp = fs::read_to_string("/Users/fredrik-alv/IdeaProjects/thomas/rs/src/input.txt")
        .expect("Something went wrong reading the file");


    let mut acc = 0;

    while !tmp.is_empty() {
        println!("Current remaining: {}", tmp);

        for number in &numbers {
            if tmp.starts_with(number.0) {
                acc += number.1;
                tmp = tmp[number.0.len()..].parse().unwrap();

                break;
            }
        }

        println!("Uh-oh incorrect priority!");
        panic("fuck")
    }

    println!("Done! Total {}", acc);
}
