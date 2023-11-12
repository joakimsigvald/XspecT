using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// TODO
/// </summary>
public abstract class Constraint<TConstraint, TActual> where TConstraint : Constraint<TConstraint, TActual>
{
    internal readonly TActual _actual;
    internal Constraint(TActual actual) => _actual = actual;
    internal ContinueWith<TConstraint> And() => new((TConstraint)this);
}