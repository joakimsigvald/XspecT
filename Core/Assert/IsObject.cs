using System.Runtime.CompilerServices;

using static Xunit.Assert;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided object
/// </summary>
public record IsObject : Constraint<object, IsObject>
{
    internal IsObject(object actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsObject> Not(
        object expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => NotSame(expected, Actual), expectedExpr);

    /// <summary>
    /// Should().BeNull()
    /// </summary>
    public ContinueWith<IsObject> Null()
        => AssertAnd(() => Xunit.Assert.Null(Actual));

    /// <summary>
    /// Should().NotBeNull()
    /// </summary>
    public ContinueWith<IsObject> NotNull()
        => AssertAnd(() => Xunit.Assert.NotNull(Actual));

    /// <summary>
    /// Should().Be(expected)
    /// </summary>
    public ContinueWith<IsObject> EqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Equal(expected, Actual), expectedExpr);

    /// <summary>
    /// Should().NotBe(expected)
    /// </summary>
    public ContinueWith<IsObject> NotEqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => NotEqual(expected, Actual), expectedExpr);

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> Like(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Equivalent(expected, Actual), expectedExpr);

    /// <summary>
    /// Should().NotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> NotLike(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => NotEqual(expected, Actual), expectedExpr);

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> EquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Equivalent(expected, Actual), expectedExpr);

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> NotEquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => NotEqual(expected, Actual), expectedExpr);
}