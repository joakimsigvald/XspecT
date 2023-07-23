using FluentAssertions;
using System.Linq.Expressions;

namespace XspecT.Verification;

public static class ListAssertionsObsolete
{
    /// <summary>
    /// actual.Should().BeEmpty()
    /// </summary>
    [Obsolete("Replaced by Is().Empty()")]
    public static AndConstraint<FluentAssertions.Collections.GenericCollectionAssertions<TValue>> IsEmpty<TValue>(
        this IEnumerable<TValue> actual)
        => actual.Should().BeEmpty();

    /// <summary>
    /// actual.Should().NotBeEmpty()
    /// </summary>
    [Obsolete("Replaced by Is().NotEmpty()")]
    public static AndConstraint<FluentAssertions.Collections.GenericCollectionAssertions<TValue>> IsNotEmpty<TValue>(
        this IEnumerable<TValue> actual)
        => actual.Should().NotBeEmpty();

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    [Obsolete("Replaced by Has().Single()")]
    public static AndConstraint<FluentAssertions.Collections.GenericCollectionAssertions<TValue>> ContainsSingle<TValue>(
        this IEnumerable<TValue> actual)
        => actual.Should().ContainSingle();

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    [Obsolete("Replaced by Has().Single()")]
    public static AndConstraint<FluentAssertions.Collections.GenericCollectionAssertions<TItem>> ContainsSingle<TItem>(
        this IEnumerable<TItem> actual, Expression<Func<TItem, bool>> predicate)
        => actual.Should().ContainSingle(predicate);

    /// <summary>
    /// actual.Should().HaveCount(expected)
    /// </summary>
    [Obsolete("Replaced by Has().Count()")]
    public static AndConstraint<FluentAssertions.Collections.GenericCollectionAssertions<TItem>> HasCount<TItem>(
        this IEnumerable<TItem> actual, int expected)
        => actual.Should().HaveCount(expected);

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    [Obsolete("Replaced by Has().Only()")]
    public static AndConstraint<FluentAssertions.Collections.GenericCollectionAssertions<(TItem, int)>> Each<TItem>(
        this IEnumerable<TItem> collection,
        Func<TItem, int, bool> predicate, string because = "", params object[] becauseArgs)
        => collection.Select((it, i) => (it, i)).
            Should().OnlyContain(t => predicate(t.it, t.i), because, becauseArgs);

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    [Obsolete("Replaced by Has().Only()")]
    public static AndConstraint<FluentAssertions.Collections.GenericCollectionAssertions<TItem>> Each<TItem>(
        this IEnumerable<TItem> collection,
        Func<TItem, bool> predicate, string because = "", params object[] becauseArgs)
        => collection.Should().OnlyContain(it => predicate(it), because, becauseArgs);
}