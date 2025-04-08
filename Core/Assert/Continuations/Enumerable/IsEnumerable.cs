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
        => Assert(Ignore.Me, NotNullAnd(Xunit.Assert.Empty)).And();

    /// <summary>
    /// Assert that the enumerable is not empty
    /// </summary>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    [Obsolete("Use Not().Empty instead")]
    public ContinueWith<IsEnumerableContinuation<TItem>> NotEmpty()
        => Assert(Ignore.Me, NotNullAnd(Xunit.Assert.NotEmpty)).And();

    /// <summary>
    /// Assert that the enumerable is null
    /// </summary>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> Null()
        => Assert(Ignore.Me, Xunit.Assert.Null).And();

    /// <summary>
    /// Assert that the enumerable is not null
    /// </summary>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    [Obsolete("Use Not().Null instead")]
    public ContinueWith<IsEnumerableContinuation<TItem>> NotNull()
        => Assert(Ignore.Me, Xunit.Assert.NotNull).And();

    /// <summary>
    /// Assert that the two enumerables do not refer to the same object
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr">Provided by the compiler for building the specification description</param>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> Not(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Not().SameAs(expected, expectedExpr);

    /// <summary>
    /// Assert that both enumerables has the same number of elements and that elements at same position are equal to each other
    /// </summary>
    /// <param name="expected">The enumerable to validate against</param>
    /// <param name="expectedExpr">Provided by the compiler for building the specification description</param>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> EqualTo(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), actual => Xunit.Assert.Equal(expected, actual), expectedExpr!).And();

    /// <summary>
    /// Assert that both enumerables are the same instance
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsEnumerableContinuation<TItem>> SameAs(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), actual => Xunit.Assert.Same(expected, actual), expectedExpr!, methodName: string.Empty).And();

    /// <summary>
    /// Assert that both enumerables are not the same instance
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    [Obsolete("Use Not().SameAs instead")]
    public ContinueWith<IsEnumerableContinuation<TItem>> NotSameAs(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), actual => Xunit.Assert.NotSame(expected, actual), expectedExpr!, methodName: "not").And();

    /// <summary>
    /// Assert that the enumerables are not equal, with regard to length, order and equality of elements
    /// </summary>
    /// <param name="expected">The enumerable to validate against</param>
    /// <param name="expectedExpr">Provided by the compiler for building the specification description</param>
    /// <returns>A continuation for making further assertions on the enumerable</returns>
    [Obsolete("Use Not().EqualTo instead")]
    public ContinueWith<IsEnumerableContinuation<TItem>> NotEqualTo(
        IEnumerable<TItem> expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), actual => Xunit.Assert.NotEqual(expected, actual), expectedExpr!).And();

    internal override IsEnumerableContinuation<TItem> Continue() => Create(Actual, ActualExpr);
}