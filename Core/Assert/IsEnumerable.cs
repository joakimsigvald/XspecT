using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class IsEnumerable<TItem> : Constraint<IsEnumerable<TItem>, IEnumerable<TItem>>
{
    internal IsEnumerable(IEnumerable<TItem> actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// actual.Should().BeEmpty()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsEnumerable<TItem>> Empty()
    {
        AddAssert([CustomAssertion] () => _actual.Should().BeEmpty(), "is empty");
        _actual.Should().BeEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeEmpty()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsEnumerable<TItem>> NotEmpty()
    {
        _actual.Should().NotBeEmpty();
        return And();
    }

    /// <summary>
    /// Verifies that the collection contains a single element and returns that element
    /// </summary>
    [CustomAssertion] public TItem Single() => Xunit.Assert.Single(_actual);

    /// <summary>
    /// actual.Should().NotBeEmpty()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsEnumerable<TItem>> NotNull()
    {
        _actual.Should().NotBeEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsEnumerable<TItem>> Not(
        IEnumerable<TItem> expected, 
        [System.Runtime.CompilerServices.CallerArgumentExpression("expected")] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => _actual.Should().NotBeSameAs(expected), "is not", expectedExpr);
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
        [System.Runtime.CompilerServices.CallerArgumentExpression("expected")] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => _actual.Should().BeEquivalentTo(expected), "is equal to", expectedExpr);
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
        _actual.Should().NotBeEquivalentTo(expected);
        return And();
    }
}