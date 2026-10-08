(* File HigherFun/ParseAndRunHigher.fs *)

module ParseAndRunHigher

open Absyn;;
open HigherFun;;

let fromString = Parse.fromString;;

let eval = HigherFun.eval;;

let run e = eval e [];;

(* Examples of higher-order programs, in concrete syntax *)

let ex5 =
    Parse.fromString
     @"let tw g = let app x = g (g x) in app end
       in let mul3 x = 3 * x
       in let quad = tw mul3
       in quad 7 end end end";;

let ex6 =
    Parse.fromString
     @"let tw g = let app x = g (g x) in app end
       in let mul3 x = 3 * x
       in let quad = tw mul3
       in quad end end end";;

let ex7 =
    Parse.fromString
     @"let rep n =
           let rep1 g =
               let rep2 x = if n=0 then x else rep (n-1) g (g x)
               in rep2 end
           in rep1 end
       in let mul3 x = 3 * x
       in let tw = rep 2
       in let quad = tw mul3
       in quad 7 end end end end";;

let ex8 =
    Parse.fromString
     @"let rep n =
           let rep1 g =
               let rep2 x = if n=0 then x else rep (n-1) g (g x)
               in rep2 end
           in rep1 end
       in let mul3 x = 3 * x
       in let twototen = rep 10 mul3
       in twototen 7 end end end";;

let ex9 =
    Parse.fromString
     @"let rep n =
           let rep1 g =
               let rep2 x = if n=0 then x else rep (n-1) g (g x)
               in rep2 end
           in rep1 end
       in let mul3 x = 3 * x
       in let twototen = (rep 10) mul3
       in twototen 7 end end end";;

(* Exercise 6.1: run the higher-order evaluator on four programs that
   build up a function step by step ("add 2" gives back a function,
   which is then called with 5). //6.1 *)

let ex61_1 = fromString
              @"let add x = let f y = x+y in f end
                in add 2 5 end";;
let ex61_1res = run ex61_1;;                (* Int 7 *)

let ex61_2 = fromString
              @"let add x = let f y = x+y in f end
                in let addtwo = add 2
                   in addtwo 5 end
                end";;
let ex61_2res = run ex61_2;;                (* Int 7, same as above *)

let ex61_3 = fromString
              @"let add x = let f y = x+y in f end
                in let addtwo = add 2
                   in let x = 77 in addtwo 5 end
                   end
                end";;
let ex61_3res = run ex61_3;;                (* Int 7: the later 'x = 77' does
                                                not change addtwo, see README *)

let ex61_4 = fromString
              @"let add x = let f y = x+y in f end
                in add 2 end";;
let ex61_4res = run ex61_4;;                (* A Closure value, not an Int:
                                                see README *)

(* Exercise 6.2 / 6.3: anonymous functions, in concrete syntax. //6.2 //6.3 *)

let ex62_1 = fromString "fun x -> 2*x";;
let ex62_1res = run (Call(ex62_1, CstI 21));;   (* Int 42 *)

let ex62_2 = fromString "let y = 22 in fun z -> z+y end";;
let ex62_2res = run (Call(ex62_2, CstI 3));;    (* Int 25 *)

(* Same two programs, written with the "fn x => ..." syntax instead. *)

let ex63_1 = fromString "fn x => 2*x";;
let ex63_1res = run (Call(ex63_1, CstI 21));;   (* Int 42 *)

(* The micro-ML examples from exercise 6.1, now written with anonymous
   functions instead of named helper functions. *)

let ex63_2 = fromString
              @"let add x = fun y -> x+y
                in add 2 5 end";;
let ex63_2res = run ex63_2;;                    (* Int 7 *)

let ex63_3 = fromString
              @"let add = fun x -> fun y -> x+y
                in add 2 5 end";;
let ex63_3res = run ex63_3;;                    (* Int 7 *)
