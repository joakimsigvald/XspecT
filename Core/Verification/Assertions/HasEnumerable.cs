using FluentAssertions;
using System.Linq.Expressions;

namespace XspecT.Verification.Assertions;

public class HasEnumerable<TItem> : Constraint<HasEnumerable<TItem>>
{
    private readonly IEnumerable<TItem> _actual;

    public HasEnumerable(IEnumerable<TItem> actual) => _actual = actual;

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public ContinueWith<HasEnumerable<TItem>> Single()
    {
        _actual.Should().ContainSingle();
        return And();
    }

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public ContinueWith<HasEnumerable<TItem>> Single(Expression<Func<TItem, bool>> predicate)
    {
        _actual.Should().ContainSingle(predicate);
        return And();
    }

    /// <summary>
    /// actual.Should().HaveCount(expected)
    /// </summary>
    public ContinueWith<HasEnumerable<TItem>> Count(int expected)
    {
        _actual.Should().HaveCount(expected);
        return And();
    }

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    public ContinueWith<HasEnumerable<TItem>> Only(
        Func<TItem, int, bool> predicate, string because = "", params object[] becauseArgs)
    {
        _actual.Select((it, i) => (it, i)).
                Should().OnlyContain(t => predicate(t.it, t.i), because, becauseArgs);
        return And();
    }

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    public ContinueWith<HasEnumerable<TItem>> Only(
        Func<TItem, bool> predicate, string because = "", params object[] becauseArgs)
    {
        _actual.Should().OnlyContain(it => predicate(it), because, becauseArgs);
        return And();
    }
}