7.4 
In Absyn.fs i added PreInc and PreDec so ++ and --. 
In Interp.fs i added what happens when the program runs them. It finds where the varaible is saved, reads the number, adds 1 (or takes 1 away for --) and saves the new number back in the same place. 

7.5 
added "++" and "--" to CLex, so they are read as one symbol each and not as two + or two - signs.
In CPar.fsy i added that ++ followed by a variable becomes PreInc and -- followed by a variable becomes PreDec 

8.1 
Ran ex11 with 8 and got the 92 queens solutions. Compiled ex03 and ex05 and wrote out the code with the C next to it.

ex03:
```
        LDARGS 1                                                      main(n)
        CALL (1, "L1")
        STOP
L1:     INCSP 1                                                       int i;
        GETBP; CSTI 1; ADD; CSTI 0; STI; INCSP -1                     i = 0;
        GOTO "L3"                                                     while (i < n) {
L2:     GETBP; CSTI 1; ADD; LDI; PRINTI; INCSP -1                       print i;
        GETBP; CSTI 1; ADD; GETBP; CSTI 1; ADD; LDI;                    i = i + 1;
        CSTI 1; ADD; STI; INCSP -1
        INCSP 0                                                       }
L3:     GETBP; CSTI 1; ADD; LDI; GETBP; CSTI 0; ADD; LDI; LT;         i < n
        IFNZRO "L2"
        INCSP -1
        RET 0
```

ex05:
```
        LDARGS 1                                                      main(n)
        CALL (1, "L1")
        STOP
L1:     INCSP 1                                                       int r;
        GETBP; CSTI 1; ADD; GETBP; CSTI 0; ADD; LDI; STI; INCSP -1    r = n;
        INCSP 1                                                       { int r;
        GETBP; CSTI 0; ADD; LDI; GETBP; CSTI 2; ADD;                    square(n, &r);
        CALL (2, "L2"); INCSP -1
        GETBP; CSTI 2; ADD; LDI; PRINTI; INCSP -1                       print r;
        INCSP -1                                                      }
        GETBP; CSTI 1; ADD; LDI; PRINTI; INCSP -1                     print r;
        INCSP -1
        RET 0
L2:     GETBP; CSTI 1; ADD; LDI; GETBP; CSTI 0; ADD; LDI;             square(i, rp)
        GETBP; CSTI 0; ADD; LDI; MUL; STI; INCSP -1                     *rp = i * i;
        INCSP 0
        RET 1
```

In ex05 you can see the inner block because it starts with INCSP 1, which makes space for the second r, and ends with INCSP -1, which throws it away again. So the two r's are saved in different places.
Traced ex03 with 4. At the start the stack is [ 5 -999 4 ]. 5 is where to go back to when main is done, -999 is just a filler value, and 4 is n. Then INCSP 1 makes space for i.
Every time the code uses a variable it first works out where it is (GETBP, CSTI, ADD) and then reads it (LDI) or saves to it (STI).
The loop first jumps down to the check i < n. If it's true it jumps back up and runs print i and i = i + 1, and then checks again. When i gets to 4 the check is false, so it removes i, goes back with RET and hits STOP.

8.3 
In Comp.fs i added ++ and -- to the compiler

8.4 
ex08 is slower because it does way many more steps each time it goes round the loop. prog1 just keeps the number on the stack and does 4 steps: put 1, subtract, copy, jump if not 0. ex08 saves i as a variable, so every time it reads or saves i it first has to work out where i is. Plus some extra steps that do nothing. It ends up being 17 steps instead of 4.
In ex13 the loop jumps down to the check first, same as before. The && and || turn into a bunch of jumps that end up putting a 0 or 1 on the stack, and then the if checks that 0 or 1 again. So you get jumps that just land on another jump, and an else that does nothing (INCSP 0).
