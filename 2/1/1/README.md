1.1 (Intro2.fs)
Had to add max, min, == and an if-expression to the class. For max/min i just eval both sides and compare with if/else, for == same thing but check if they're equal. for if i added a new case If(cond, e1, e2) to the type and in eval i evaluate cond first, if its not 0 i run e1 else e2.

1.2 (AExpr.fs)
This one's the same expr language but without let, so just CstI, Var, Add, Sub, Mul. to print it out (fmt) i just recurse down and stick "(" + ... + " + " + ... + ")" together depending on the operator. for simplify i go through each case and check: is one side 0 (for add), are both sides just numbers (then i can compute it directly), otherwise just rebuild it. for deriv i went through the actual derivative rules by hand, constenants go to 0, the variable i'm deriving by goes to 1 (else 0), add/sub just derive both sides, mul is the product rule (e1'*e2 + e1*e2').

1.4 (Expr.cs)
Asks for the same thing as 1.2 but done with classes instead of an f# type, so every expression kind is its own class inheriting from Expr, and instead of pattern matching each class just implements its own ToString/Eval/Simplify. Logicwise its identical to what i did in AExpr.fs, just written with if-statements and inheritance instead of match cases.

2.1/2.2/2.3 (Intcomp2.fs)
Here let has to bind more than one variable at once, one after another, so e.g. let x1=.. x2=.. in .. end instead of just one. I changed Let to take a list of bindings instead of a single one. for eval i loop through the list and add each variable to the environment before moving to the next one, so later bindings can use earlier ones. freevars needed the same idea - go trough the bindings left to right, and a bindings right side only counts as "free" if it's not already been bound by something earlier in the list. tcomp (the compiler part) i just turn each binding into its own TLet, nesting them one inside the other, and left texpr/teval alone since the exercise says they don't need to change.
