using Shouldly;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided object
/// </summary>
public record IsObject : Constraint<object, IsObject>
{
    internal IsObject(object actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// ShouldNotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsObject> Not(
        object expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.ShouldNotBeSameAs(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// ShouldBeNull()
    /// </summary>
    public ContinueWith<IsObject> Null()
    {
        Assert(() => Actual.ShouldBeNull());
        return And();
    }

    /// <summary>
    /// ShouldNotBeNull()
    /// </summary>
    public ContinueWith<IsObject> NotNull()
    {
        Assert(() => Actual.ShouldNotBeNull());
        return And();
    }

    /// <summary>
    /// ShouldBe(expected)
    /// </summary>
    public ContinueWith<IsObject> EqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.ShouldBe(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// ShouldNotBe(expected)
    /// </summary>
    public ContinueWith<IsObject> NotEqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.ShouldNotBe(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// ShouldBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> Like(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.ShouldBeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// ShouldNotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> NotLike(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.ShouldNotBeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// ShouldBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> EquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.ShouldBeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// ShouldBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> NotEquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.ShouldNotBeEquivalentTo(expected), expectedExpr);
        return And();
    }
}