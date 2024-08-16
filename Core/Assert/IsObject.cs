using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided object
/// </summary>
public record IsObject : Constraint<IsObject, object>
{
    internal IsObject(object actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsObject> Not(
        object expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeSameAs(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Should().BeNull()
    /// </summary>
    public ContinueWith<IsObject> Null()
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeNull());
        Actual.Should().BeNull();
        return And();
    }

    /// <summary>
    /// Should().NotBeNull()
    /// </summary>
    public ContinueWith<IsObject> NotNull()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeNull());
        return And();
    }

    /// <summary>
    /// Should().Be(expected)
    /// </summary>
    public ContinueWith<IsObject> EqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().Be(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Should().NotBe(expected)
    /// </summary>
    public ContinueWith<IsObject> NotEqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBe(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> Like(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Should().NotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> NotLike(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> EquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> NotEquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeEquivalentTo(expected), expectedExpr);
        return And();
    }
}