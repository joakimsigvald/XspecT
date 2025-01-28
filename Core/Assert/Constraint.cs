using System.Runtime.CompilerServices;
using XspecT.Internal.Specification;

namespace XspecT.Assert;

/// <summary>
/// 
/// </summary>
public record Constraint
{
    internal string ActualExpr { get; set; }
    internal string AuxiliaryVerb { get; set; }
}

/// <summary>
/// Base class for object that allows a chain of assertions to be made on the provided value
/// </summary>
public abstract record Constraint<TActual, TContinuation>
    : Constraint
    where TContinuation : Constraint<TActual, TContinuation>, new()
{
    internal Constraint() : base() => AuxiliaryVerb = typeof(TContinuation).Name.ToWords()[0];

    internal static TContinuation Create(TActual actual, string actualExpr = null)
        => new() { Actual = actual, ActualExpr = actualExpr.ParseActual() };

    internal TActual Actual { get; private set; }

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