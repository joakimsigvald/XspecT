namespace XspecT.Verification.Assertions;

public abstract class Constraint<TConstraint> where TConstraint : Constraint<TConstraint>
{
    protected ContinueWith<TConstraint> And() => new((TConstraint)this);
}