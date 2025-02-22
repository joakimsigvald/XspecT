using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations.Enumerable;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
public record HasEnumerable<TItem> : Constraint<IEnumerable<TItem>, HasEnumerableContinuation<TItem>>
{
    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Single()
        => Assert("element", () => Xunit.Assert.Single(Actual), null, "have").And();

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Single(
        Func<TItem, bool> condition, [CallerArgumentExpression(nameof(condition))] string expectedExpr = null)
        => Assert(
            "element satisfying the condition", 
            () => Xunit.Assert.Single(Actual, new Predicate<TItem>(condition)), 
            expectedExpr, 
            "have").And();

    /// <summary>
    /// actual.Should().HaveCount(expected)
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Count(
        int expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => Xunit.Assert.Equal(expected, Actual.Count()), expectedExpr).And();

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, int, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
        => Assert(() => Xunit.Assert.DoesNotContain(
            Actual.Select((it, i) => (it, i)), t => !predicate(t.it, t.i)),
            predicateExpr)
        .And();

    /// <summary>
    /// collection.Should().OnlyContain(it => predicate(it))
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
        => Assert(() => Xunit.Assert.DoesNotContain(Actual, it => !predicate(it)), predicateExpr).And();

    /// <summary>
    /// Applies the given assertion to all element of the enumerable
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr"></param>
    /// <returns></returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Action<TItem, int> assert, [CallerArgumentExpression(nameof(assert))] string assertExpr = null)
        => Assert(() => Actual.Select((it, i) => (it, i)).ToList().ForEach(t => assert(t.it, t.i)), assertExpr).And();

    /// <summary>
    /// Applies the given assertion to all element of the enumerable
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr"></param>
    /// <returns></returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(Action<TItem> assert, [CallerArgumentExpression(nameof(assert))] string assertExpr = null)
        => Assert(() => Actual.ToList().ForEach(assert), assertExpr).And();

    internal override HasEnumerableContinuation<TItem> Continue() => Create(Actual);

    private protected override string ActualString
        => Actual is null ? "null"
        : Actual.Any() ? $"[{string.Join(", ", Actual)}]"
        : "empty";
}