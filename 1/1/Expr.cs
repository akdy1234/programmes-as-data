// Exercise 1.4: the aexpr language represented as a C# class hierarchy

using System;
using System.Collections.Generic;

// (i) Abstract syntax as a class hierarchy, with ToString() for formatting

abstract class Expr
{
    public abstract override string ToString();
    public abstract int Eval(Dictionary<string, int> env);          // (iii)
    public abstract Expr Simplify();                                 // (iv)
}
// same stuff as AExpr.fs, just as classes instead of a datatype

class CstI : Expr
{
    public readonly int Value;
    public CstI(int i) { Value = i; }

    public override string ToString() => Value.ToString();
    public override int Eval(Dictionary<string, int> env) => Value;
    public override Expr Simplify() => new CstI(Value);
}

class Var : Expr
{
    protected readonly string name;
    public Var(string name) { this.name = name; }

    public override string ToString() => name;
    public override int Eval(Dictionary<string, int> env) => env[name];
    public override Expr Simplify() => new Var(name);
}

abstract class Binop : Expr
{
    protected readonly Expr e1, e2;
    protected Binop(Expr e1, Expr e2) { this.e1 = e1; this.e2 = e2; }
}

class Add : Binop
{
    public Add(Expr e1, Expr e2) : base(e1, e2) { }

    public override string ToString() => "(" + e1 + " + " + e2 + ")";
    public override int Eval(Dictionary<string, int> env) => e1.Eval(env) + e2.Eval(env);

    // same simplify logic as before, just written as if-statements here
    public override Expr Simplify()
    {
        Expr s1 = e1.Simplify(), s2 = e2.Simplify();
        if (s1 is CstI c1 && c1.Value == 0) return s2;
        if (s2 is CstI c2 && c2.Value == 0) return s1;
        if (s1 is CstI a && s2 is CstI b) return new CstI(a.Value + b.Value);
        return new Add(s1, s2);
    }
}

class Mul : Binop
{
    public Mul(Expr e1, Expr e2) : base(e1, e2) { }

    public override string ToString() => "(" + e1 + " * " + e2 + ")";
    public override int Eval(Dictionary<string, int> env) => e1.Eval(env) * e2.Eval(env);

    public override Expr Simplify()
    {
        Expr s1 = e1.Simplify(), s2 = e2.Simplify();
        if (s1 is CstI z1 && z1.Value == 0) return new CstI(0);
        if (s2 is CstI z2 && z2.Value == 0) return new CstI(0);
        if (s1 is CstI o1 && o1.Value == 1) return s2;
        if (s2 is CstI o2 && o2.Value == 1) return s1;
        if (s1 is CstI a && s2 is CstI b) return new CstI(a.Value * b.Value);
        return new Mul(s1, s2);
    }
}

class Sub : Binop
{
    public Sub(Expr e1, Expr e2) : base(e1, e2) { }

    public override string ToString() => "(" + e1 + " - " + e2 + ")";
    public override int Eval(Dictionary<string, int> env) => e1.Eval(env) - e2.Eval(env);

    public override Expr Simplify()
    {
        Expr s1 = e1.Simplify(), s2 = e2.Simplify();
        if (s2 is CstI z && z.Value == 0) return s1;
        if (s1 is CstI a && s2 is CstI b) return new CstI(a.Value - b.Value);
        return new Sub(s1, s2);
    }
}

class MainClass
{
    static void Main()
    {
        // (i)
        Expr e = new Add(new CstI(17), new Var("z"));
        Console.WriteLine(e.ToString());

        // (ii) three more expressions in abstract syntax
        Expr e2 = new Sub(new Var("v"), new Add(new Var("w"), new Var("z")));
        Expr e3 = new Mul(new CstI(2), new Sub(new Var("v"), new Add(new Var("w"), new Var("z"))));
        Expr e4 = new Add(new Add(new Var("x"), new Var("y")), new Var("z"));
        Console.WriteLine(e2.ToString());
        Console.WriteLine(e3.ToString());
        Console.WriteLine(e4.ToString());

        // (iii) evaluation
        var env = new Dictionary<string, int> { { "v", 10 }, { "w", 3 }, { "z", 4 }, { "x", 1 }, { "y", 2 } };
        Console.WriteLine("e2 = " + e2.Eval(env));
        Console.WriteLine("e3 = " + e3.Eval(env));

        // (iv) simplification
        Expr e5 = new Add(new Var("x"), new CstI(0));
        Expr e6 = new Mul(new Add(new CstI(1), new CstI(0)), new Add(new Var("x"), new CstI(0)));
        Console.WriteLine(e5.Simplify().ToString());  // x
        Console.WriteLine(e6.Simplify().ToString());  // x
    }
}
