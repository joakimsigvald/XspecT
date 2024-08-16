using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
/// <typeparam name="TItem"></typeparam>
public record IsEnumerable<TItem> : Constraint<IsEnumerableContinuation<TItem>, IEnumerable<TItem>>
{
    internal IsEnumerable(IEnumerable<TItem> actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// actual.Should().BeEmpty()
    /// </summary>
    public ContinueWith<IsEnumerableContinuation<TItem>> Empty()
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeEmpty());
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeEmpty()
    /// </summary>
    public ContinueWith<IsEnumerableContinuation<TItem>> NotEmpty()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeEmpty());
        return And();
    }

    /// <summary>
    /// Verifies that the collection contains a single element and returns that element
    /// </summary>
    [Obsolete("Use Has().Single() instead")]
    public TItem Single() => Xunit.Assert.Single(Actual);

    /// <summary>
    /// actual.Should().NotBeNull()
    /// </summary>
    public ContinueWith<IsEnumerableContinuation<TItem>> NotNull()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeNull());
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsEnumerableContinuation<TItem>> Not(
        IEnumerable<TItem> expected, 
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeSameAs(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Verifies that the both collection contain the same number of elements and that elements on same position are equal to each other
    /// </summary>
    /// <param name="expected">The collection to validate against</param>
    /// <param name="expectedExpr"></param>
    /// <returns>A continuation for making further assertions</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> EqualTo(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Verifies that the collections are not equal
    /// </summary>
    /// <param name="expected">The collection to validate against</param>
    /// <param name="expectedExpr"></param>
    /// <returns>A continuation for making further assertions</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> NotEqualTo(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeEquivalentTo(expected), expectedExpr);
        return And();
    }

    internal override IsEnumerableContinuation<TItem> Continue() => new(Actual);
}