using System;
using System.Runtime.CompilerServices;
using XspecT.Internal;
using XspecT.Internal.TestData;
using Xunit.Sdk;

namespace XspecT;

/// <summary>
/// 
/// </summary>
public static class Specification
{
    [ThreadStatic]
    private static SpecificationBuilder _builder;

    internal static void Clear() => _builder = null;

    /// <summary>
    /// 
    /// </summary>
    public static string Description => Builder.Description;

    internal static void AddMockSetup<TService>(string callExpr) 
        => Builder.Add(() => Builder.AddMockSetup<TService>(callExpr));

    internal static void AddMockReturns(string returnsExpr) 
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

    internal static void AddAssert(
        Action assert,
        string actual = null,
        string expected = null,
        [CallerMemberName] string verb = "")
    {
        Builder.Add(() => Builder.AddAssert(actual, verb, expected));
        try
        {
            assert();
        }
        catch (XunitException ex)
        {
            throw new XunitException(Description, ex);
        }
    }

    internal static void AddThen() => Builder.Add(Builder.AddThen);

    internal static void AddGiven(string valueExpr, ApplyTo applyTo) 
        => Builder.Add(() => Builder.AddGiven(valueExpr, applyTo));

    internal static void AddGiven<TModel>(string setupExpr, string article = null)
        => Builder.Add(() => Builder.AddGiven<TModel>(setupExpr, article));

    internal static void AddVerify<TService>(string expressionExpr) 
        => Builder.Add(() => Builder.AddVerify<TService>(expressionExpr));

    internal static void AddAssertThrows<TError>(string binder = null)
        => Builder.Add(() => Builder.AddAssertThrows<TError>(binder));

    internal static void AddAssertThrows(string expectedExpr)
        => Builder.Add(() => Builder.AddAssertThrows(expectedExpr));

    internal static void AddTap(string expr) => Builder.Add(() => Builder.AddTap(expr));

    internal static void AddMockReturnsDefault<TService>(string returnsExpr)
         => Builder.Add(() => Builder.AddMockReturnsDefault<TService>(returnsExpr));

    internal static void AddAssert([CallerMemberName] string assertName = null)
         => Builder.Add(() => Builder.AddAssert(assertName));

    private static SpecificationBuilder Builder => _builder ??= new();
}