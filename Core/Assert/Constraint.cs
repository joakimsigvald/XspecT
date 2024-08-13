namespace XspecT.Assert;

/// <summary>
/// 
/// </summary>
public record Constraint(string ActualExpr) {}

/// <summary>
/// Base class for object that allows a chain of assertions to be made on the provided value
/// </summary>
public abstract record Constraint<TConstraint, TActual>(TActual Actual, string ActualExpr) 
    : Constraint(ActualExpr)
    where TConstraint : Constraint<TConstraint, TActual>
{
    internal ContinueWith<TConstraint> And() => new(Continue());
    internal virtual TConstraint Continue() => (TConstraint)this;

    internal void AddAssert(Action assert, string verb, string expectedExpr = null)
        => Specification.AddAssert(assert, ActualExpr, expectedExpr, verb);

}