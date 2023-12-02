using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class IsEnumerable<TItem> : Constraint<IsEnumerable<TItem>, IEnumerable<TItem>>
{
    internal IsEnumerable(IEnumerable<TItem> actual) : base(actual) { }

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
    /// actual.Should().NotBeEmpty()
    /// </summary>
    public ContinueWith<IsEnumerable<TItem>> NotNull()
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