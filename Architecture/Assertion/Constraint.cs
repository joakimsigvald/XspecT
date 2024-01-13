namespace XspecT.Architecture.Assertion;

/// <summary>
/// Base class for object that allows a chain of assertions to be made on the provided value
/// </summary>
public abstract class Constraint<TConstraint, TActual> where TConstraint : Constraint<TConstraint, TActual>
{
    internal readonly TActual _actual;
    internal Constraint(TActual actual) => _actual = actual;
    internal ContinueWith<TConstraint> And() => new(Continue());
    internal virtual TConstraint Continue() => (TConstraint)this;
}