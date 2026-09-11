module Assignments

let add (x: int) (y: int) : int = x+y

// Exercise 2.4

type sinstr =
    | SCstI of int
    | SVar of int
    | SAdd
    | SSub
    | SMul
    | SPop
    | SSwap

let sinstrToInt (instr: sinstr) : int list =
    match instr with
    | SCstI x -> [0; x]
    | SVar x  -> [1; x]
    | SAdd    -> [2]
    | SSub    -> [3]
    | SMul    -> [4]
    | SPop    -> [5]
    | SSwap   -> [6]

let assemble (instrs: sinstr list) : int list =
    List.collect sinstrToInt instrs

let intsToFile (inss: int list) (fname: string) =
    let text = String.concat " " (List.map string inss)
    System.IO.File.WriteAllText(fname, text)