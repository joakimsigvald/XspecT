namespace XspecT.Verification.Assertions;

public abstract class Constraint<TConstraint, TActual> where TConstraint : Constraint<TConstraint, TActual>
{
    protected readonly TActual Actual;
    protected Constraint(TActual actual) => Actual = actual;
    protected ContinueWith<TConstraint> And() => new((TConstraint)this);
}