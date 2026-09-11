(* Exercise 1.2: arithmetic expressions without let-bindings *)

module AExpr

(* (i) Alternative datatype for arithmetic expressions *)

type aexpr =
  | CstI of int
  | Var of string
  | Add of aexpr * aexpr
  | Mul of aexpr * aexpr
  | Sub of aexpr * aexpr;;

(* (ii) Representations of v - (w+z) and 2*(v-(w+z)) and x+y+z+v *)

let ex1 = Sub(Var "v", Add(Var "w", Var "z"));;

let ex2 = Mul(CstI 2, Sub(Var "v", Add(Var "w", Var "z")));;

(* + associates to the left, so x+y+z+v is ((x+y)+z)+v *)
let ex3 = Add(Add(Add(Var "x", Var "y"), Var "z"), Var "v");;

(* (iii) Format an aexpr as a string, fully parenthesized *)

let rec fmt (e : aexpr) : string =
    match e with
    | CstI i        -> string i
    | Var x          -> x
    | Add(e1, e2)    -> "(" + fmt e1 + " + " + fmt e2 + ")"
    | Sub(e1, e2)    -> "(" + fmt e1 + " - " + fmt e2 + ")"
    | Mul(e1, e2)    -> "(" + fmt e1 + " * " + fmt e2 + ")";;

let ex4 = Sub(Var "x", CstI 34);;
let ex4s = fmt ex4;;    (* "(x - 34)" *)

(* (iv) Expression simplification *)

// gets rid of the useless zeros/ones and just does the math if it's numbers
let rec simplify (e : aexpr) : aexpr =
    match e with
    | CstI i -> CstI i
    | Var x  -> Var x
    | Add(e1, e2) ->
        match simplify e1, simplify e2 with
        | CstI 0, s2       -> s2
        | s1, CstI 0       -> s1
        | CstI a, CstI b   -> CstI (a + b)
        | s1, s2           -> Add(s1, s2)
    | Sub(e1, e2) ->
        match simplify e1, simplify e2 with
        | s1, CstI 0       -> s1
        | CstI a, CstI b   -> CstI (a - b)
        | s1, s2 when s1 = s2 -> CstI 0   (* more ambitious: e - e -> 0 *)
        | s1, s2           -> Sub(s1, s2)
    | Mul(e1, e2) ->
        match simplify e1, simplify e2 with
        | CstI 0, _        -> CstI 0
        | _, CstI 0        -> CstI 0
        | CstI 1, s2       -> s2
        | s1, CstI 1       -> s1
        | CstI a, CstI b   -> CstI (a * b)
        | s1, s2           -> Mul(s1, s2);;

let ex5  = Add(Var "x", CstI 0);;
let ex5s = simplify ex5;;              (* Var "x" *)
let ex6  = Add(CstI 1, CstI 0);;
let ex6s = simplify ex6;;              (* CstI 1 *)
let ex7  = Mul(Add(CstI 1, CstI 0), Add(Var "x", CstI 0));;
let ex7s = simplify ex7;;              (* Var "x" *)

(* (v) Symbolic differentiation with respect to a single variable *)

// normal derivative rules, nothing fancy
let rec deriv (e : aexpr) (x : string) : aexpr =
    match e with
    | CstI _ -> CstI 0
    | Var y  -> if y = x then CstI 1 else CstI 0
    | Add(e1, e2) -> Add(deriv e1 x, deriv e2 x)
    | Sub(e1, e2) -> Sub(deriv e1 x, deriv e2 x)
    | Mul(e1, e2) -> Add(Mul(deriv e1 x, e2), Mul(e1, deriv e2 x));;

(* d/dx (x*x + 3*x) = 2*x + 3, shown here unsimplified then simplified *)
let ex8  = Add(Mul(Var "x", Var "x"), Mul(CstI 3, Var "x"));;
let ex8d = deriv ex8 "x";;
let ex8ds = fmt (simplify ex8d);;
