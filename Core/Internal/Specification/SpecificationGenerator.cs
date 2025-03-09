using System.Runtime.CompilerServices;
using XspecT.Internal.TestData;
using Xunit.Sdk;

namespace XspecT.Internal.Specification;

internal static class SpecificationGenerator
{
    [ThreadStatic]
    private static SpecificationBuilder? _builder;

    internal static void Clear() => _builder = null;

    internal static void AddMockSetup<TService>(string callExpr)
        => Builder.Add(() => Builder.AddMockSetup<TService>(callExpr));

    internal static void AddMockReturns(string? returnsExpr = null)
        => Builder.Add(() => Builder.AddMockReturns(returnsExpr));

    internal static void AddMockThrowsDefault<TService, TError>()
        => Builder.Add(Builder.AddMockThrowsDefault<TService, TError>);

    internal static void AddMockThrowsDefault<TService>(string expectedExpr)
        => Builder.Add(() => Builder.AddMockThrowsDefault<TService>(expectedExpr));

    internal static void AddMockThrows<TError>()
        => Builder.Add(Builder.AddMockThrows<TError>);

    internal static void AddMockThrows(string expectedExpr)
        => Builder.Add(() => Builder.AddMockThrows(expectedExpr));

    internal static void AddWhen(string actExpr) => Builder.Add(() => Builder.AddWhen(actExpr));

    internal static void AddAfter(string setUpExpr) => Builder.Add(() => Builder.AddAfter(setUpExpr));

    internal static void AddBefore(string tearDownExpr) => Builder.Add(() => Builder.AddBefore(tearDownExpr));

    internal static void Assert(
        Action assert,
        string actual,
        string expected,
        string verb)
    {
        Builder.Add(() => Builder.AddAssert(actual, verb, expected));
        try
        {
            Builder.SuppressRecording();
            assert();
            Builder.InciteRecording();
        }
        catch (XunitException ex)
        {
            throw new XunitException(Builder.Specification, ex);
        }
    }

    internal static void AddThen() => Builder.Add(Builder.AddThen);

    internal static void AddGiven(string valueExpr, ApplyTo applyTo)
        => Builder.Add(() => Builder.AddGiven(valueExpr, applyTo));

    internal static void AddGiven<TModel>(string setupExpr, string? article = null)
        => Builder.Add(() => Builder.AddGiven<TModel>(setupExpr, article));

    internal static void AddGivenCount<TModel>(string count)
        => Builder.Add(() => Builder.AddGivenCount<TModel>(count));

    internal static void AddVerify<TService>(string expressionExpr)
        => Builder.Add(() => Builder.AddVerify<TService>(expressionExpr));

    internal static void AddAssertThrows<TError>(string? binder = null)
        => Builder.Add(() => Builder.AddAssertThrows<TError>(binder));

    internal static void AddAssertThrows(string expectedExpr)
        => Builder.Add(() => Builder.AddAssertThrows(expectedExpr));

    internal static void AddTap(string expr) => Builder.Add(() => Builder.AddTap(expr));

    internal static void AddMockReturnsDefault<TService>(string returnsExpr)
         => Builder.Add(() => Builder.AddMockReturnsDefault<TService>(returnsExpr));

    internal static void AddAssert([CallerMemberName] string assertName = null)
         => Builder.Add(() => Builder.AddAssert(assertName));

    internal static void AddAssertConjunction(string conjunction)
         => Builder.Add(() => Builder.AddAssertConjunction(conjunction));

    internal static void Init(string prologue) => Builder.Init(prologue);

    internal static SpecificationBuilder Builder => _builder ??= new();
}