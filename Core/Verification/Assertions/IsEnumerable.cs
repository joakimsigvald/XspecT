using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsEnumerable<TItem> : Constraint<IsEnumerable<TItem>, IEnumerable<TItem>>
{
    public IsEnumerable(IEnumerable<TItem> actual) : base(actual) { }

    /// <summary>
    /// actual.Should().BeEmpty()
    /// </summary>
    public ContinueWith<IsEnumerable<TItem>> Empty()
    {
        Actual.Should().BeEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeEmpty()
    /// </summary>
    public ContinueWith<IsEnumerable<TItem>> NotEmpty()
    {
        Actual.Should().NotBeEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsEnumerable<TItem>> Not(IEnumerable<TItem> expected)
    {
        Actual.Should().NotBeSameAs(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsEnumerable<TItem>> EqualTo(IEnumerable<TItem> expected)
    {
        Actual.Should().BeEquivalentTo(expected);
        return And();
    }
}