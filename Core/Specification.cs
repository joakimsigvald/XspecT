using FluentAssertions.Common;
using System.Linq.Expressions;
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

    internal static void AddWhen<TDelegate>(Expression<TDelegate> act) 
        => Builder.Add(() => Builder.AddWhen(act));

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

    internal static void AddAnd() => Builder.Add(Builder.AddAnd);

    internal static void AddThen(string subjectExpr) 
        => Builder.Add(() => Builder.AddThen(subjectExpr));

    internal static void AddGiven(string valueExpr, ApplyTo applyTo) 
        => Builder.Add(() => Builder.AddGiven(valueExpr, applyTo));

    internal static void AddGivenSetup<TModel>(string setupExpr)
        => Builder.Add(() => Builder.AddGivenSetup<TModel>(setupExpr));

    internal static void AddVerify<TService>(string expressionExpr) 
        => Builder.Add(() => Builder.AddVerify<TService>(expressionExpr));

    internal static void AddThrows<TError>() => Builder.Add(Builder.AddThrows<TError>);

    internal static void AddTap(string expr) => Builder.Add(() => Builder.AddTap(expr));

    private static SpecificationBuilder Builder => _builder ??= new();
}