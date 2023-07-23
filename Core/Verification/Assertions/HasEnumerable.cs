using FluentAssertions;
using FluentAssertions.Collections;
using System.Linq.Expressions;

namespace XspecT.Verification.Assertions;

public class HasEnumerable<TItem>
{
    private readonly IEnumerable<TItem> _actual;

    public HasEnumerable(IEnumerable<TItem> actual) => _actual = actual;

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public AndConstraint<GenericCollectionAssertions<TItem>> Single() => _actual.Should().ContainSingle();

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public AndConstraint<GenericCollectionAssertions<TItem>> Single(Expression<Func<TItem, bool>> predicate)
        => _actual.Should().ContainSingle(predicate);

    /// <summary>
    /// actual.Should().HaveCount(expected)
    /// </summary>
    public AndConstraint<GenericCollectionAssertions<TItem>> Count(int expected)
        => _actual.Should().HaveCount(expected);

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    public AndConstraint<GenericCollectionAssertions<(TItem, int)>> Only(
        Func<TItem, int, bool> predicate, string because = "", params object[] becauseArgs)
        => _actual.Select((it, i) => (it, i)).
            Should().OnlyContain(t => predicate(t.it, t.i), because, becauseArgs);

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    public AndConstraint<GenericCollectionAssertions<TItem>> Only(
        Func<TItem, bool> predicate, string because = "", params object[] becauseArgs)
        => _actual.Should().OnlyContain(it => predicate(it), because, becauseArgs);
}