using System.Runtime.CompilerServices;
using static Xunit.Assert;

namespace XspecT.Assert.Continuations;

/// <summary>
/// Object that allows an assertions to be made on the provided object
/// </summary>
public record IsObject : Constraint<object, IsObject>
{
    /// <summary>
    /// Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsObject> Not(
        object expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => NotSame(expected, Actual), expectedExpr).And();

    /// <summary>
    /// Should().BeNull()
    /// </summary>
    public ContinueWith<IsObject> Null()
        => Assert(() => Xunit.Assert.Null(Actual)).And();

    /// <summary>
    /// Should().NotBeNull()
    /// </summary>
    public ContinueWith<IsObject> NotNull()
        => Assert(() => Xunit.Assert.NotNull(Actual)).And();

    /// <summary>
    /// Should().Be(expected)
    /// </summary>
    public ContinueWith<IsObject> EqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => Equal(expected, Actual), expectedExpr).And();

    /// <summary>
    /// Should().NotBe(expected)
    /// </summary>
    public ContinueWith<IsObject> NotEqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => NotEqual(expected, Actual), expectedExpr).And();

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> Like(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => Equivalent(expected, Actual), expectedExpr).And();

    /// <summary>
    /// Should().NotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> NotLike(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => NotEqual(expected, Actual), expectedExpr).And();

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> EquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => Equivalent(expected, Actual), expectedExpr).And();

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> NotEquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => NotEqual(expected, Actual), expectedExpr).And();
}