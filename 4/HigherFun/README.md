# Compiling and loading the higher-order micro-ML evaluator

This folder is a separate copy of the micro-ML language, kept apart
from the `Fun` folder. `Fun/Absyn.fs` was changed by exercises 4.3 and
4.4 so a function call can take several arguments at once. Chapters 5
and 6 are about functions returning functions (currying), which only
works if calling a function really applies it to one argument at a
time. So this folder keeps the original, one-argument version of the
language instead, and is where exercises 6.1, 6.2 and 6.3 live.

## A. Loading the higher-order evaluator, with abstract syntax only

```bash
dotnet fsi Absyn.fs HigherFun.fs
```

```fsharp
open HigherFun;;
eval ex1 [];;
```

```fsharp
#q;;
```

## B. Build the lexer and parser

```bash
dotnet build parse.fsproj
```

This generates `FunLex.fs`/`FunLex.fsi` and `FunPar.fs`/`FunPar.fsi`
from `FunLex.fsl` and `FunPar.fsy`.

```bash
dotnet fsi -r bin/Debug/net10.0/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs
```

```fsharp
open Parse;;
let e1 = fromString "let f x = x + 7 in f 2 end";;
```

```fsharp
#q;;
```

## C. Using the lexer, parser and higher-order evaluator together (exercise 6.1)

```bash
dotnet fsi -r bin/Debug/net10.0/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs HigherFun.fs ParseAndRunHigher.fs
```

```fsharp
open ParseAndRunHigher;;
run (fromString @"let twice f = let g x = f(f(x)) in g end
                  in let mul3 z = z*3 in twice mul3 2 end end");;
```

```fsharp
ex61_1res;;
ex61_2res;;
ex61_3res;;
ex61_4res;;
```

```fsharp
#q;;
```

## D. Anonymous functions (exercises 6.2 and 6.3)

```fsharp
open ParseAndRunHigher;;
ex62_1res;;   (* fun x -> 2*x       applied to 21 *)
ex62_2res;;   (* fun z -> z+y       applied to 3, with y = 22 *)
ex63_1res;;   (* fn x => 2*x        applied to 21 *)
ex63_2res;;   (* add 2 5, written with "fun" instead of a named helper *)
ex63_3res;;   (* add 2 5, written as add = fun x -> fun y -> x+y *)
```

```fsharp
#q;;
```

## E. Using the lexer, parser and polymorphic type inference together

```bash
dotnet fsi -r bin/Debug/net10.0/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs TypeInference.fs ParseAndType.fs
```

```fsharp
open ParseAndType;;
inferType (fromString "let f x = 1 in f 7 + f false end");;
```

```fsharp
#q;;
```
