using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// Base class for object that allows a chain of assertions to be made on the provided value
/// </summary>
public abstract class Constraint<TConstraint, TActual> where TConstraint : Constraint<TConstraint, TActual>
{
    internal readonly TActual _actual;
    private readonly string _actualExpr;

    internal Constraint(TActual actual, string actualExpr = null)
    {
        _actual = actual;
        _actualExpr = actualExpr;
    }

    [CustomAssertion] internal ContinueWith<TConstraint> And() => new(Continue());
    internal virtual TConstraint Continue() => (TConstraint)this;

    internal void AddAssert(Action assert, string expectedExpr, string verb) 
        => Specification.AddAssert(assert, _actualExpr, expectedExpr, verb);

}