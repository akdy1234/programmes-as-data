(* HigherFun/Absyn.fs * Abstract syntax for micro-ML, a functional language

   This is a separate copy of Absyn.fs, kept apart from the one in the
   Fun folder. The Fun folder's Absyn.fs was changed by exercises 4.3
   and 4.4 so that functions can take several arguments at once
   (Letfun/Call use a list of names/arguments instead of just one).

   Chapter 6 (exercises 6.1, 6.2, 6.3) is about functions returning
   functions (currying), which only works if a function call really
   takes just one argument at a time. So this copy keeps the original,
   one-argument version of Letfun and Call, instead of the list-based
   version from exercise 4.3/4.4.
*)

module Absyn

type expr =
  | CstI of int
  | CstB of bool
  | Var of string
  | Let of string * expr * expr
  | Prim of string * expr * expr
  | If of expr * expr * expr
  | Letfun of string * string * expr * expr    (* (f, x, fBody, letBody) *)
  | Call of expr * expr
  | Fun of string * expr                       (* anonymous function (x, body) *) //6.2
