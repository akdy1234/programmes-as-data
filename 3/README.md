I put "//x.y" on my changes/additions to the files

3.6 
Added string -> machine code method compString to Expr.fs

3.7
If else then added to Expr

4.3 (Absyn.fs + Fun.fs)
Functions can now take multiple arguments instead of exactly one. Letfun/Call/Closure changed from a single value to a list

4.4 
Grammar extended so multi arg functions in FunPar.fsy. rewrote AppExpr so "f a b" parses as one flat Call(f, [a; b]) instead of nested calls.
