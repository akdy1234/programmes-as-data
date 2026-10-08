**2.4** Solved and is in Intcomp1.fs


**2.5** is presolved from what i can tell


**3.2** is 3.2.png


**3.3,** using the gramar: 

0: Main 

1: Expr EOF (A)

2: LET NAME EQ Expr IN Expr END EOF (F, for the skeleton)

3: LET NAME EQ Expr IN Expr PLUS Expr END EOF (H, for addition)

4: LET NAME EQ Expr IN Expr PLUS Expr TIMES Expr END EOF (G, for multiplication)

5: LET NAME EQ Expr IN Expr PLUS Expr TIMES CSTINT(3) END EOF (C, constant)

6: LET NAME EQ Expr IN Expr PLUS CSTINT(2) TIMES CSTINT(3) END EOF (C, constant)

7: LET NAME EQ Expr IN NAME(z) PLUS CSTINT(2) TIMES CSTINT(3) END EOF (B, name/variable)

8: LET NAME EQ LAPR Expr RPAR IN NAME(z) PLUS CSTINT(2) TIMES CSTINT(3) END EOF (E, parenthesis)

9: LET NAME EQ LAPR CSTINT(17) RPAR IN NAME(z) PLUS CSTINT(2) TIMES CSTINT(3) END EOF (C, constant)


Evaluates to: AFHGCCBEC


**3.4** is 3.4.png - sorry for the horrible drawing, i hope it is readable