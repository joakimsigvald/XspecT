using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations.Enumerable;

/// <summary>
/// Continuation that allows an assertions to be made on the provided enumerable
/// </summary>
/// <typeparam name="TItem"></typeparam>
public record IsEnumerable<TItem> : EnumerableConstraint<TItem, IsEnumerableContinuation<TItem>>
{
    /// <summary>
    /// Assert that the enumerable is empty
    /// </summary>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> Empty()
        => Assert(null, () => Xunit.Assert.Empty(Actual), null).And();

    /// <summary>
    /// Assert that the enumerable is not empty
    /// </summary>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> NotEmpty()
        => Assert(null, () => Xunit.Assert.NotEmpty(Actual), null).And();

    /// <summary>
    /// Assert that the enumerable is null
    /// </summary>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> Null()
        => Assert(null, () => Xunit.Assert.Null(Actual), null).And();

    /// <summary>
    /// Assert that the enumerable is not null
    /// </summary>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> NotNull()
        => Assert(null, () => Xunit.Assert.NotNull(Actual), null).And();

    /// <summary>
    /// Assert that the two enumerables do not refer to the same object
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr">Provided by the compiler for building the specification description</param>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> Not(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(Describe(expected), () => Xunit.Assert.NotSame(expected, Actual), expectedExpr).And();

    /// <summary>
    /// Assert that both enumerables has the same number of elements and that elements at same position are equal to each other
    /// </summary>
    /// <param name="expected">The enumerable to validate against</param>
    /// <param name="expectedExpr">Provided by the compiler for building the specification description</param>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> EqualTo(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(Describe(expected), () => Xunit.Assert.Equal(expected, Actual), expectedExpr).And();

    /// <summary>
    /// Assert that the enumerables are not equal, with regard to length, order and equality of elements
    /// </summary>
    /// <param name="expected">The enumerable to validate against</param>
    /// <param name="expectedExpr">Provided by the compiler for building the specification description</param>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> NotEqualTo(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(Describe(expected), () => Xunit.Assert.NotEqual(expected, Actual), expectedExpr).And();

    internal override IsEnumerableContinuation<TItem> Continue() => Create(Actual);
}