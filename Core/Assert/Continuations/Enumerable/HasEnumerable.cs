using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations.Enumerable;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
public record HasEnumerable<TItem> : EnumerableConstraint<TItem, HasEnumerableContinuation<TItem>>
{
    /// <summary>
    /// Assert that the enumerable contains a single item
    /// </summary>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the single item</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, TItem> Single()
    {
        TItem? theItem = default;
        return Assert("element",
            NotNullAnd(actual => theItem = Xunit.Assert.Single(actual)), string.Empty, "have")
            .AndThat(theItem!);
    }

    /// <summary>
    /// Assert that the enumerable contains a single item satisfying the given condition
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable or accessing the single item</returns>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, TItem> Single(
        Func<TItem, bool> condition, [CallerArgumentExpression(nameof(condition))] string? expectedExpr = null)
    {
        TItem? theItem = default;
        return Assert(
                "element satisfying the condition",
                NotNullAnd(actual => theItem = Xunit.Assert.Single(actual, new Predicate<TItem>(condition))),
                expectedExpr!,
                "have")
            .AndThat(theItem!);
    }

    /// <summary>
    /// Assert that the enumerable has the given count
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable</returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> Count(
        int expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(expected, NotNullAnd(actual => Xunit.Assert.Equal(expected, actual.Count())), expectedExpr!, "have").And();

    /// <summary>
    /// Assert that the all the items of the enumerable satisfy the given indexed condition.
    /// Pass if empty.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable</returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, int, bool> condition, [CallerArgumentExpression(nameof(condition))] string? expectedExpr = null)
        => Assert("elements satisfying the condition",
            NotNullAnd(actual => Xunit.Assert.DoesNotContain(actual.Select((it, i) => (it, i)), t => !condition(t.it, t.i))),
            expectedExpr!,
            "have")
        .And();

    /// <summary>
    /// Assert that the all the items of the enumerable satisfy the given condition.
    /// Pass if empty.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="expectedExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable</returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, bool> condition, [CallerArgumentExpression(nameof(condition))] string? expectedExpr = null)
        => Assert(
            "elements satisfying the condition",
            NotNullAnd(actual => Xunit.Assert.DoesNotContain(actual, it => !condition(it))),
            expectedExpr!,
            "have").And();

    /// <summary>
    /// Assert that the all the items of the enumerable satisfy the given index assertion.
    /// Pass if empty.
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable</returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Action<TItem, int> assert, [CallerArgumentExpression(nameof(assert))] string? assertExpr = null)
        => Assert(
            "elements satisfying the assertion",
            NotNullAnd(actual => actual.Select((it, i) => (it, i)).ToList().ForEach(t => assert(t.it, t.i))),
            assertExpr!,
            "have").And();

    /// <summary>
    /// Assert that the all the items of the enumerable satisfy the given assertion.
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr">Ignore, provided by runtime</param>
    /// <returns>A continuation for making additional asserts on the enumerable</returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Action<TItem> assert, [CallerArgumentExpression(nameof(assert))] string? assertExpr = null)
        => Assert(
            "elements satisfying the assertion",
            NotNullAnd(actual => actual.ToList().ForEach(assert)), assertExpr!,
            "have").And();

    internal override HasEnumerableContinuation<TItem> Continue() => Create(Actual, ActualExpr);
}