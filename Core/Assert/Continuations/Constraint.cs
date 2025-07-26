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
    internal bool Inverted { private protected get; init; } = false;
    internal bool IsEither { get; set; } = false;
    internal Exception? Exception { get; set; }
}

/// <summary>
/// Base class for object that allows a chain of assertions to be made on the provided value
/// </summary>
public abstract record Constraint<TActual, TContinuation>
    : Constraint
    where TContinuation : Constraint<TActual, TContinuation>, new()
{
    internal Constraint() : base() => AuxiliaryVerb = typeof(TContinuation).Name.ToWords()[0];

    /// <summary>
    /// Invert the following assertion
    /// </summary>
    /// <returns></returns>
    public TContinuation Not()
        => new()
        {
            Actual = Actual,
            ActualExpr = ActualExpr,
            Inverted = true,
            IsEither = IsEither,
            AuxiliaryVerb = $"{AuxiliaryVerb} not"
        };

    internal TContinuation Either
        => new()
        {
            Actual = Actual,
            ActualExpr = ActualExpr,
            Inverted = Inverted,
            AuxiliaryVerb = $"{AuxiliaryVerb} either",
            IsEither = true
        };

    /// <summary>
    /// Asserts that the string is equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<TContinuation> OneOf(
        TActual[] values,
        [CallerArgumentExpression(nameof(values))] string? expectedExpr = null)
        => Assert(
            $"[{string.Join(", ", values.Select(v => Describe(v)))}]",
            actual => Xunit.Assert.Contains(actual, values),
            expectedExpr!).And();

    internal static TContinuation Create(TActual? actual, string actualExpr)
        => new() { Actual = actual, ActualExpr = actualExpr.ParseActual() };

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
        string? expectedExpr = null,
        string auxVerb = "be",
        VerbalizationStrategy verbalizationStrategy = VerbalizationStrategy.None,
        [CallerMemberName] string? methodName = null)
    {
        if (!Inverted && verbalizationStrategy == VerbalizationStrategy.PresentSingularS)
            AuxiliaryVerb = string.Empty;
        return Assert(() =>
            {
                Xunit.Sdk.XunitException? xuex = null;
                try
                {
                    assert(Actual);
                    if (!Inverted)
                        return;
                }
                catch (Exception ex)
                {
                    if (Inverted)
                        return;
                    xuex = ex as Xunit.Sdk.XunitException;
                }
                Exception ??= GetException(xuex);
                if (IsEither)
                    return;

                throw Exception;
            }, methodName!, expectedExpr, Inverted ? VerbalizationStrategy.None : verbalizationStrategy);

        Xunit.Sdk.XunitException GetException(Xunit.Sdk.XunitException? innerEx)
            => new Xunit.Sdk.XunitException(
                    $"Expected {ActualExpr} to {GetExpectation()} but found {Describe(Actual, methodName!)}",
                    GetExpectedException(innerEx));

        string GetExpectation() => $"{GetVerb()} {expected}".Trim();

        string GetVerb() => $"{GetAuxVerb()} {methodName!.AsWords()}".Trim();

        string GetAuxVerb() =>
            ((Inverted ? "not " : string.Empty)
            + (verbalizationStrategy == VerbalizationStrategy.PresentSingularS ? string.Empty : auxVerb))
            .TrimEnd();
    }

    private static Xunit.Sdk.XunitException? GetExpectedException(Xunit.Sdk.XunitException? ex)
        => ex is null || ex.Message.StartsWith("Expected")
            ? ex
            : GetExpectedException(ex.InnerException as Xunit.Sdk.XunitException);

    private protected virtual string Describe(TActual? value, string? methodName = null) => value?.ToString() ?? "null";

    internal ContinueWith<TContinuation> And() => new(Continue());
    internal ContinueWithThat<TContinuation, TThat> AndThat<TThat>(TThat that) => new(Continue(), that);
    internal virtual TContinuation Continue() => (TContinuation)this;

    private protected TContinuation Assert(
        Action assert,
        string methodName,
        string? expected,
        VerbalizationStrategy verbalizationStrategy = VerbalizationStrategy.None)
    {
        var verb = $"{AuxiliaryVerb} {methodName.AsWords(verbalizationStrategy)}".Trim();
        SpecificationGenerator.Assert(assert, ActualExpr, expected, verb);
        return (TContinuation)this;
    }
}