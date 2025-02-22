using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations.Enumerable;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
public record HasEnumerable<TItem> : EnumerableConstraint<TItem, HasEnumerableContinuation<TItem>>
{
    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, TItem> Single()
    {
        TItem theItem = default;
        return Assert("element", () => theItem = Xunit.Assert.Single(Actual), null, "have")
            .AndThat(theItem);
    }

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public ContinueWithThat<HasEnumerableContinuation<TItem>, TItem> Single(
        Func<TItem, bool> condition, [CallerArgumentExpression(nameof(condition))] string expectedExpr = null)
    {
        TItem theItem = default;
        return Assert(
                "element satisfying the condition",
                () => theItem = Xunit.Assert.Single(Actual, new Predicate<TItem>(condition)),
                expectedExpr,
                "have")
            .AndThat(theItem);
    }

    /// <summary>
    /// actual.Should().HaveCount(expected)
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Count(
        int expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(expected, () => Xunit.Assert.Equal(expected, Actual.Count()), expectedExpr, "have").And();

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, int, bool> condition, [CallerArgumentExpression(nameof(condition))] string expectedExpr = null)
        => Assert("elements satisfying the condition", 
            () => Xunit.Assert.DoesNotContain(Actual.Select((it, i) => (it, i)), t => !condition(t.it, t.i)),
            expectedExpr, 
            "have")
        .And();

    /// <summary>
    /// collection.Should().OnlyContain(it => predicate(it))
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, bool> condition, [CallerArgumentExpression(nameof(condition))] string expectedExpr = null)
        => Assert(
            "elements satisfying the condition", 
            () => Xunit.Assert.DoesNotContain(Actual, it => !condition(it)), 
            expectedExpr,
            "have").And();

    /// <summary>
    /// Applies the given assertion to all element of the enumerable
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr"></param>
    /// <returns></returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Action<TItem, int> assert, [CallerArgumentExpression(nameof(assert))] string assertExpr = null)
        => Assert(
            "elements satisfying the condition",
            () => Actual.Select((it, i) => (it, i)).ToList().ForEach(t => assert(t.it, t.i)), 
            assertExpr,
            "have").And();

    /// <summary>
    /// Applies the given assertion to all element of the enumerable
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr"></param>
    /// <returns></returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(Action<TItem> assert, [CallerArgumentExpression(nameof(assert))] string assertExpr = null)
        => Assert(
            "elements satisfying the condition", 
            () => Actual.ToList().ForEach(assert), assertExpr, 
            "have").And();

    internal override HasEnumerableContinuation<TItem> Continue() => Create(Actual);
}