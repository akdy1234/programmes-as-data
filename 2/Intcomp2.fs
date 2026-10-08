(* Exercises 2.1-2.3: expr extended with multiple SEQUENTIAL let-bindings

     let x1 = e1  x2 = e2  ...  xn = en in ebody end

   Each ei may refer to x1 .. x(i-1) (the bindings made so far) but not
   to itself; the scope of x1 .. xn is just ebody.

   Reuses lookup, union, minus, getindex, texpr and teval from Intcomp1 --
   Exercise 2.3 explicitly says texpr/teval need no changes. *)

module Intcomp2

open Intcomp1

(* ---------------------------------------------------------------------- *)
(* Exercise 2.1: expr with multiple sequential let-bindings, and eval *)

type expr =
  | CstI of int
  | Var of string
  | Let of (string * expr) list * expr    (* CHANGED: list of bindings *)
  | Prim of string * expr * expr;;

let e1 = Let(["x1", CstI 5], Prim("+", Var "x1", CstI 7));;

(* let x1 = 5+7   x2 = x1*2 in x1+x2 end *)
let e2 =
    Let(["x1", Prim("+", CstI 5, CstI 7);
         "x2", Prim("*", Var "x1", CstI 2)],
        Prim("+", Var "x1", Var "x2"));;

let rec eval (e : expr) (env : (string * int) list) : int =
    match e with
    | CstI i -> i
    | Var x  -> lookup env x
    | Let(bindings, ebody) ->
        // just loops through the bindings, each one gets added before the next runs
        let env1 =
            List.fold (fun envAcc (x, erhs) -> (x, eval erhs envAcc) :: envAcc)
                      env bindings
        eval ebody env1
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim _             -> failwith "unknown primitive";;

let e1v = eval e1 [];;    (* 12 *)
let e2v = eval e2 [];;    (* 12 + 24 = 36 *)


(* ---------------------------------------------------------------------- *)
(* Exercise 2.2: freevars revised for multiple sequential let-bindings *)

let rec freevars (e : expr) : string list =
    match e with
    | CstI i -> []
    | Var x  -> [x]
    | Prim(ope, e1, e2) -> union (freevars e1, freevars e2)
    | Let(bindings, ebody) ->
        (* fold left to right: a binding's right-hand side can only be
           free in variables not already bound by EARLIER bindings *)
        let (freeInBindings, boundSoFar) =
            List.fold (fun (free, bound) (x, erhs) ->
                           let freeHere = minus (freevars erhs, bound)
                           (union (free, freeHere), union (bound, [x])))
                      ([], [])
                      bindings
        union (freeInBindings, minus (freevars ebody, boundSoFar));;

(* let x1 = x1+7 in x1+8 end has free variable x1 (only the first x1) *)
let e3 = Let(["x1", Prim("+", Var "x1", CstI 7)], Prim("+", Var "x1", CstI 8));;
let e3free = freevars e3;;    (* ["x1"] *)

let e2free = freevars e2;;    (* [] -- e2 above is closed *)


(* ---------------------------------------------------------------------- *)
(* Exercise 2.3: tcomp revised for multiple sequential let-bindings.
   texpr and teval are unchanged, reused directly from Intcomp1: a chain
   of sequential bindings is compiled as nested single-variable TLet's. *)

let rec tcomp (e : expr) (cenv : string list) : texpr =
    match e with
    | CstI i -> TCstI i
    | Var x  -> TVar (getindex cenv x)
    | Prim(ope, e1, e2) -> TPrim(ope, tcomp e1 cenv, tcomp e2 cenv)
    | Let(bindings, ebody) ->
        // same idea, just one TLet per binding stacked on top of each other
        let rec compBindings bindings cenv =
            match bindings with
            | []                 -> tcomp ebody cenv
            | (x, erhs) :: rest  -> TLet(tcomp erhs cenv, compBindings rest (x :: cenv))
        compBindings bindings cenv;;

(* Correctness check: teval (tcomp e []) [] should equal eval e [] *)
let e1t  = tcomp e1 [];;
let e1tv = teval e1t [];;   (* should equal e1v = 12 *)

let e2t  = tcomp e2 [];;
let e2tv = teval e2t [];;   (* should equal e2v = 36 *)
