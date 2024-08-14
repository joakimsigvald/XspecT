using FluentAssertions;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
public record HasEnumerable<TItem> : Constraint<HasEnumerableContinuation<TItem>, IEnumerable<TItem>>
{
    internal HasEnumerable(IEnumerable<TItem> actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<HasEnumerableContinuation<TItem>> Single()
    {
        Actual.Should().ContainSingle();
        return And();
    }

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<HasEnumerableContinuation<TItem>> Single(
        Expression<Func<TItem, bool>> expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Actual.Should().ContainSingle(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().HaveCount(expected)
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Count(
        int expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().HaveCount(expected), expectedExpr);
        Actual.Should().HaveCount(expected);
        return And();
    }

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    [CustomAssertion]
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, int, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
    {
        Actual.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i));
        return And();
    }

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    [CustomAssertion]
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
    {
        Actual.Should().OnlyContain(it => predicate(it));
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
        Actual.Select((it, i) => (it, i)).ToList().ForEach(t => assert(t.it, t.i));
        return And();
    }

    /// <summary>
    /// Applies the given assertion to all element of the enumerable
    /// </summary>
    /// <param name="assert"></param>
    /// <returns></returns>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(Action<TItem> assert, [CallerArgumentExpression(nameof(assert))] string assertExpr = null)
    {
        Actual.ToList().ForEach(assert);
        return And();
    }

    internal override HasEnumerableContinuation<TItem> Continue() => new(Actual);
}