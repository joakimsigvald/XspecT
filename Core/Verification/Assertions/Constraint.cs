namespace XspecT.Verification.Assertions;

public abstract class Constraint<TConstraint, TActual> where TConstraint : Constraint<TConstraint, TActual>
{
    internal readonly TActual _actual;
    internal Constraint(TActual actual) => _actual = actual;
    internal ContinueWith<TConstraint> And() => new((TConstraint)this);
}