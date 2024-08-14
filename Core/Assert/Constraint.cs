using System.Runtime.CompilerServices;
using XspecT.Internal;

namespace XspecT.Assert;

/// <summary>
/// 
/// </summary>
public record Constraint(string ActualExpr, string AuxiliaryVerb) {}

/// <summary>
/// Base class for object that allows a chain of assertions to be made on the provided value
/// </summary>
public abstract record Constraint<TConstraint, TActual>(
    TActual Actual, string ActualExpr) 
    : Constraint(ActualExpr, typeof(TConstraint).Name.ToWords()[0])
    where TConstraint : Constraint<TConstraint, TActual>
{
    internal ContinueWith<TConstraint> And() => new(Continue());
    internal virtual TConstraint Continue() => (TConstraint)this;

    internal void AddAssert(
        Action assert, 
        string expectedExpr = null,
        string verb = null,
        [CallerMemberName] string methodName = null)
        => Specification.AddAssert(
            assert, ActualExpr, expectedExpr, verb ?? $"{AuxiliaryVerb} {methodName.AsWords()}".Trim());
}