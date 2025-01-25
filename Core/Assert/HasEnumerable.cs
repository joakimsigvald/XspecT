using Shouldly;
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
    /// actual.ShouldHaveSingleItem()
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Single()
    {
        Assert(() => Actual.ShouldHaveSingleItem());
        return And();
    }

    /// <summary>
    /// actual.ShouldHaveSingleItem()
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Single(
        Expression<Func<TItem, bool>> expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Where(expected.Compile()).ShouldHaveSingleItem(), expectedExpr);
        return And();
    }

    /// <summary>
    /// actual.ShouldHaveCount(expected)
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> Count(
        int expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Count().ShouldBe(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// collection.Select((it, i) => (it, i)).ShouldOnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, int, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
    {
        Assert(() => Actual.Select((it, i) => (it, i)).Where(t => !predicate(t.it, t.i)).ShouldBeEmpty(), predicateExpr);
        return And();
    }

    /// <summary>
    /// collection.ShouldOnlyContain(it => predicate(it))
    /// </summary>
    public ContinueWith<HasEnumerableContinuation<TItem>> All(
        Func<TItem, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
    {
        Assert(() => Actual.Where(it => !predicate(it)).ShouldBeEmpty(), predicateExpr);
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
        Assert(() => Actual.ToList().ForEach(assert), assertExpr);
        return And();
    }

    internal override HasEnumerableContinuation<TItem> Continue() => new(Actual);
}