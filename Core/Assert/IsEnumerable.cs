using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Continuation that allows an assertions to be made on the provided enumerable
/// </summary>
/// <typeparam name="TItem"></typeparam>
public record IsEnumerable<TItem> : Constraint<IEnumerable<TItem>, IsEnumerableContinuation<TItem>>
{
    internal IsEnumerable(IEnumerable<TItem> actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Assert that the enumerable is empty
    /// </summary>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> Empty()
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeEmpty());
        return And();
    }

    /// <summary>
    /// Assert that the enumerable is not empty
    /// </summary>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> NotEmpty()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeEmpty());
        return And();
    }

    /// <summary>
    /// Assert that the enumerable is null
    /// </summary>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> Null()
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeNull());
        return And();
    }

    /// <summary>
    /// Assert that the enumerable is not null
    /// </summary>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> NotNull()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeNull());
        return And();
    }

    /// <summary>
    /// Assert that the two enumerables do not refer to the same object
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr">Provided by the compiler for building the specification description</param>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> Not(
        IEnumerable<TItem> expected, 
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeSameAs(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Assert that both enumerables has the same number of elements and that elements at same position are equal to each other
    /// </summary>
    /// <param name="expected">The enumerable to validate against</param>
    /// <param name="expectedExpr">Provided by the compiler for building the specification description</param>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> EqualTo(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Assert that the enumerables are not equal, with regard to length, order and equality of elements
    /// </summary>
    /// <param name="expected">The enumerable to validate against</param>
    /// <param name="expectedExpr">Provided by the compiler for building the specification description</param>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> NotEqualTo(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeEquivalentTo(expected), expectedExpr);
        return And();
    }

    internal override IsEnumerableContinuation<TItem> Continue() => new(Actual);
}