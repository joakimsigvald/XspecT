using System.Runtime.CompilerServices;
using XspecT.Internal.Specification;

namespace XspecT.Assert.Continuations;

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

    internal virtual ContinueWith<TContinuation> Value(
        TActual expected, string expectedExpr)
        => Assert(expected, () => Xunit.Assert.Equal(expected, Actual), expectedExpr, methodName: string.Empty)
        .And();

    private protected Constraint<TActual, TContinuation> Assert(
        object expected,
        Action assert,
        string expectedExpr,
        string auxVerb = "be",
        [CallerMemberName] string methodName = null)
        => Assert(() =>
        {
            try
            {
                assert();
            }
            catch (Exception ex)
            {
                var expectationStr = $"{methodName.AsWords()} {expected}".Trim();
                throw new AssertionFailed($"Expected {ActualExpr} to {auxVerb} {expectationStr} but found {Describe(Actual)}", GetInnermost(ex as AssertionFailed));
            }
        }, expectedExpr, methodName: methodName);

    private static AssertionFailed GetInnermost(AssertionFailed? ex)
        => ex?.InnerException is null ? ex : GetInnermost(ex.InnerException as AssertionFailed);

    private protected virtual string Describe(TActual value) => value?.ToString() ?? "null";

    internal ContinueWith<TContinuation> And() => new(Continue());
    internal ContinueWithThat<TContinuation, TThat> AndThat<TThat>(TThat that) => new(Continue(), that);
    internal virtual TContinuation Continue() => (TContinuation)this;

    private protected TContinuation Assert(
        Action assert,
        string expectedExpr = null,
        string verb = null,
        [CallerMemberName] string methodName = null)
    {
        SpecificationGenerator.Assert(
                assert, ActualExpr, expectedExpr, verb ?? $"{AuxiliaryVerb} {methodName.AsWords()}".Trim());
        return (TContinuation)this;
    }
}