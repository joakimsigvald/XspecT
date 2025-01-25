using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
public record HasEnumerable<TItem> : Constraint<IEnumerable<TItem>, HasEnumerableContinuation<TItem>>
{
    internal HasEnumerable(IEnumerable<TItem> actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Single()
        => AssertAnd(() => Xunit.Assert.Single(Actual));

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Single(
        Expression<Func<TItem, bool>> expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Xunit.Assert.Single(Actual, new Predicate<TItem>(expected.Compile())), expectedExpr);

    /// <summary>
    /// actual.Should().HaveCount(expected)
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Count(
        int expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Xunit.Assert.Equal(expected, Actual.Count()), expectedExpr);

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, int, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
        => AssertAnd(() => Xunit.Assert.DoesNotContain(Actual.Select((it, i) => (it, i)), t => !predicate(t.it, t.i)), predicateExpr);

    /// <summary>
    /// collection.Should().OnlyContain(it => predicate(it))
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
        => AssertAnd(() => Xunit.Assert.DoesNotContain(Actual, it => !predicate(it)), predicateExpr);

    /// <summary>
    /// Applies the given assertion to all element of the enumerable
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr"></param>
    /// <returns></returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Action<TItem, int> assert, [CallerArgumentExpression(nameof(assert))] string assertExpr = null)
        => AssertAnd(() => Actual.Select((it, i) => (it, i)).ToList().ForEach(t => assert(t.it, t.i)), assertExpr);

    /// <summary>
    /// Applies the given assertion to all element of the enumerable
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr"></param>
    /// <returns></returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(Action<TItem> assert, [CallerArgumentExpression(nameof(assert))] string assertExpr = null)
        => AssertAnd(() => Actual.ToList().ForEach(assert), assertExpr);

    internal override HasEnumerableContinuation<TItem> Continue() => new(Actual);
}