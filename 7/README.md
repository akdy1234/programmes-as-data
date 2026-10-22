8.5
Added conditional expressions (e1 ? e2 : e3) to micro-C
Added Cond to Absyn.fs, added ? and : as tokens in CLex.fsl and the grammar rule for it in CPar.fsy, and compiled it in Comp.fs the same way as if/else, just using cExpr instead of cstmt so the branch that runs leaves its value on the stack.
tested with ex_cond.c, and normal and nested ternaries both gave the right answers.

8.6
Added switch statements to micro-C
Added Switch to Absyn.fs, switch/case as keywords in CLex.fsl, and the grammar for it in CPar.fsy.
In Comp.fs i compiled it like a chain of if-checks against the switch value, where each case jumps straight to the end after running, so there's no fall-through to the next case.
Tested with ex_switch.c  got the right days for month 1-4, and month 5 correctly skipped

9.1 
Compiled Selsort.cs with csc /o and disassembled it with ildasm into Selsort.il, and compiled Selsort.java and disassembled it with javap into Selsort.jvmbytecode. Deleted extra stuff and put notes on the instructions
.il and .jvmbytecode can be found in Virtual\Selsort

9.3 
The broken one keeps a permanent "dummy" field pointing at the very first node ever put in the queue which is never cleared so it builds forever. Dummy still points to the start of the chain, so the garbage collector can never free any of them. 
In QueueFixed.java i removed the separate dummy field and just used the first node to initialize head/tail directly, so once head moves past it there's nothing left pointing to it