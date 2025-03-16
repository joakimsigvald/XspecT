using System.Runtime.CompilerServices;
using XspecT.Internal.Specification;

namespace XspecT.Assert.Continuations;

/// <summary>
/// 
/// </summary>
public record Constraint
{
    /// <summary>
    /// 
    /// </summary>
    internal string ActualExpr { get; set; } = "!UNDESCRIBED!";
    internal string? AuxiliaryVerb { get; set; }
}

/// <summary>
/// Base class for object that allows a chain of assertions to be made on the provided value
/// </summary>
public abstract record Constraint<TActual, TContinuation>
    : Constraint
    where TContinuation : Constraint<TActual, TContinuation>, new()
{
    internal Constraint() : base() => AuxiliaryVerb = typeof(TContinuation).Name.ToWords()[0];

    private protected bool Inverted { get; init; } = false;

    internal static TContinuation Create(TActual? actual, string actualExpr, bool inverted = false)
        => new() { Actual = actual, ActualExpr = actualExpr.ParseActual(), Inverted = inverted };

    internal TActual? Actual { get; private set; }

    internal virtual ContinueWith<TContinuation> Value(
        TActual expected, string expectedExpr)
        => Assert(expected, actual => Xunit.Assert.Equal(expected, actual), expectedExpr, methodName: string.Empty)
        .And();

    private protected static Action<TActual?> NotNullAnd(Action<TActual> assert)
        => actual =>
        {
            Xunit.Assert.NotNull(actual);
            assert(actual);
        };

    private protected Action<TActual?> AssertNot(Action<TActual?> assert)
        => actual =>
        {
            try
            {
                assert(Actual);
            }
            catch (Xunit.Sdk.XunitException)
            {
                return;
            }
            Xunit.Assert.Fail();
        };

    private protected Constraint<TActual, TContinuation> Assert(
        object? expected,
        Action<TActual?> assert,
        string expectedExpr,
        string auxVerb = "be",
        string? verb = null,
        [CallerMemberName] string? methodName = null)
    {
        //if (Inverted)
        //    auxVerb = $"not {auxVerb}".TrimEnd();
        return Assert(() =>
            {
                try
                {
                    assert(Actual);
                }
                catch (Exception ex)
                {
                    if (Inverted)
                        return;
                    Fail(ex);
                }
                if (Inverted)
                    Fail();

                void Fail(Exception? ex = null)
                {
                    var verbStr = $"{auxVerb} {methodName!.AsWords()}".Trim();
                    var expectationStr = $"{verbStr} {expected}".Trim();
                    throw new Xunit.Sdk.XunitException(
                        $"Expected {ActualExpr} to {expectationStr} but found {Describe(Actual)}",
                        GetExpectedAsException(ex as Xunit.Sdk.XunitException));
                }
            }, new() { Expected = expectedExpr, Verb = verb, MethodName = methodName! });
    }

    private static Xunit.Sdk.XunitException? GetExpectedAsException(Xunit.Sdk.XunitException? ex)
        => ex is null || ex.Message.StartsWith("Expected")
            ? ex
            : GetExpectedAsException(ex.InnerException as Xunit.Sdk.XunitException);

    private protected virtual string Describe(TActual? value) => value?.ToString() ?? "null";

    internal ContinueWith<TContinuation> And() => new(Continue());
    internal ContinueWithThat<TContinuation, TThat> AndThat<TThat>(TThat that) => new(Continue(), that);
    internal virtual TContinuation Continue() => (TContinuation)this;

    private protected TContinuation Assert(
        Action assert,
        AssertExpression expression)
    {
        SpecificationGenerator.Assert(
                assert, ActualExpr, expression.Expected, expression.Verb ?? $"{AuxiliaryVerb} {expression.MethodName.AsWords()}".Trim());
        return (TContinuation)this;
    }
}

internal class AssertExpression
{
    internal required string Expected { get; init; }
    internal string? Verb { get; init; }
    internal required string MethodName { get; init; }
}