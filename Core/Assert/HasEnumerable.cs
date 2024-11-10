using FluentAssertions;
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
    {
        Assert([CustomAssertion] () => Actual.Should().ContainSingle());
        return And();
    }

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Single(
        Expression<Func<TItem, bool>> expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Actual.Should().ContainSingle(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// actual.Should().HaveCount(expected)
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Count(
        int expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Actual.Should().HaveCount(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, int, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
    {
        Assert([CustomAssertion] () => Actual.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i)), predicateExpr);
        return And();
    }

    /// <summary>
    /// collection.Should().OnlyContain(it => predicate(it))
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
    {
        Assert([CustomAssertion] () => Actual.Should().OnlyContain(it => predicate(it)), predicateExpr);
        return And();
    }

    /// <summary>
    /// Applies the given assertion to all element of the enumerable
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr"></param>
    /// <returns></returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Action<TItem, int> assert, [CallerArgumentExpression(nameof(assert))] string assertExpr = null)
    {
        Assert(() => Actual.Select((it, i) => (it, i)).ToList().ForEach(t => assert(t.it, t.i)), assertExpr);
        return And();
    }

    /// <summary>
    /// Applies the given assertion to all element of the enumerable
    /// </summary>
    /// <param name="assert"></param>
    /// <param name="assertExpr"></param>
    /// <returns></returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(Action<TItem> assert, [CallerArgumentExpression(nameof(assert))] string assertExpr = null)
    {
        Assert([CustomAssertion] () => Actual.ToList().ForEach(assert), assertExpr);
        return And();
    }

    internal override HasEnumerableContinuation<TItem> Continue() => new(Actual);
}