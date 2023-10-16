using FluentAssertions;
using System.Linq.Expressions;

namespace XspecT.Verification.Assertions;

public class HasEnumerable<TItem> : Constraint<HasEnumerable<TItem>, IEnumerable<TItem>>
{
    public HasEnumerable(IEnumerable<TItem> actual) : base(actual) { }

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public ContinueWith<HasEnumerable<TItem>> Single()
    {
        Actual.Should().ContainSingle();
        return And();
    }

    /// <summary>
    /// actual.Should().ContainSingle()
    /// </summary>
    public ContinueWith<HasEnumerable<TItem>> Single(Expression<Func<TItem, bool>> predicate)
    {
        Actual.Should().ContainSingle(predicate);
        return And();
    }

    /// <summary>
    /// actual.Should().HaveCount(expected)
    /// </summary>
    public ContinueWith<HasEnumerable<TItem>> Count(int expected)
    {
        Actual.Should().HaveCount(expected);
        return And();
    }

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    public ContinueWith<HasEnumerable<TItem>> Only(
        Func<TItem, int, bool> predicate, string because = "", params object[] becauseArgs)
    {
        Actual.Select((it, i) => (it, i)).
                Should().OnlyContain(t => predicate(t.it, t.i), because, becauseArgs);
        return And();
    }

    /// <summary>
    /// collection.Select((it, i) => (it, i)).Should().OnlyContain(t => predicate(t.it, t.i))
    /// </summary>
    public ContinueWith<HasEnumerable<TItem>> Only(
        Func<TItem, bool> predicate, string because = "", params object[] becauseArgs)
    {
        Actual.Should().OnlyContain(it => predicate(it), because, becauseArgs);
        return And();
    }
}