using FluentAssertions;
using FluentAssertions.Collections;

namespace XspecT.Verification.Assertions;

public class IsEnumerable<TItem> : Constraint<IsEnumerable<TItem>>
{
    private IEnumerable<TItem> _actual;

    public IsEnumerable(IEnumerable<TItem> actual) => _actual = actual;

    /// <summary>
    /// actual.Should().BeEmpty()
    /// </summary>
    public ContinueWith<IsEnumerable<TItem>> Empty()
    {
        _actual.Should().BeEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeEmpty()
    /// </summary>
    public ContinueWith<IsEnumerable<TItem>> NotEmpty()
    {
        _actual.Should().NotBeEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsEnumerable<TItem>> Not(IEnumerable<TItem> expected)
    {
        _actual.Should().NotBeSameAs(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsEnumerable<TItem>> EqualTo(IEnumerable<TItem> expected)
    {
        _actual.Should().BeEquivalentTo(expected);
        return And();
    }
}