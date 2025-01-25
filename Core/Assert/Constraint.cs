using System.Runtime.CompilerServices;
using XspecT.Internal.Specification;

namespace XspecT.Assert;

/// <summary>
/// 
/// </summary>
public record Constraint(string ActualExpr, string AuxiliaryVerb)
{
}

/// <summary>
/// Base class for object that allows a chain of assertions to be made on the provided value
/// </summary>
public abstract record Constraint<TActual, TContinuation>(
    TActual Actual, string ActualExpr)
    : Constraint(ActualExpr, typeof(TContinuation).Name.ToWords()[0])
    where TContinuation : Constraint<TActual, TContinuation>
{
    internal ContinueWith<TContinuation> AssertAnd(
        Action assert,
        string expectedExpr = null,
        string verb = null,
        [CallerMemberName] string methodName = null)
    {
        Assert(assert, expectedExpr, verb, methodName);
        return And();
    }

    internal ContinueWith<TContinuation> And() => new(Continue());
    internal virtual TContinuation Continue() => (TContinuation)this;

    internal void Assert(
        Action assert,
        string expectedExpr = null,
        string verb = null,
        [CallerMemberName] string methodName = null)
        => SpecificationGenerator.Assert(
            assert, ActualExpr, expectedExpr, verb ?? $"{AuxiliaryVerb} {methodName.AsWords()}".Trim());
}