using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
/// <typeparam name="TItem"></typeparam>
public record IsEnumerable<TItem> : Constraint<IsEnumerable<TItem>, IEnumerable<TItem>>
{
    internal IsEnumerable(IEnumerable<TItem> actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// actual.Should().BeEmpty()
    /// </summary>
    public ContinueWith<IsEnumerable<TItem>> Empty()
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeEmpty());
        Actual.Should().BeEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeEmpty()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsEnumerable<TItem>> NotEmpty()
    {
        Actual.Should().NotBeEmpty();
        return And();
    }

    /// <summary>
    /// Verifies that the collection contains a single element and returns that element
    /// </summary>
    public TItem Single() => Xunit.Assert.Single(Actual);

    /// <summary>
    /// actual.Should().NotBeEmpty()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsEnumerable<TItem>> NotNull()
    {
        Actual.Should().NotBeEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsEnumerable<TItem>> Not(
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
    public ContinueWith<IsEnumerable<TItem>> EqualTo(
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
    /// <returns>A continuation for making further assertions</returns>
    [CustomAssertion]
    public ContinueWith<IsEnumerable<TItem>> NotEqualTo(IEnumerable<TItem> expected)
    {
        Actual.Should().NotBeEquivalentTo(expected);
        return And();
    }
}