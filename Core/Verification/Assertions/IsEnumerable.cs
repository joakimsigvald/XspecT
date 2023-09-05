using FluentAssertions;
using FluentAssertions.Collections;

namespace XspecT.Verification.Assertions;

public class IsEnumerable<TItem>
{
    private IEnumerable<TItem> _actual;

    public IsEnumerable(IEnumerable<TItem> actual) => _actual = actual;

    /// <summary>
    /// actual.Should().BeEmpty()
    /// </summary>
    public AndConstraint<GenericCollectionAssertions<TItem>> Empty() => _actual.Should().BeEmpty();

    /// <summary>
    /// actual.Should().NotBeEmpty()
    /// </summary>
    public AndConstraint<GenericCollectionAssertions<TItem>> NotEmpty() => _actual.Should().NotBeEmpty();

    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    public AndConstraint<GenericCollectionAssertions<TItem>> Not(IEnumerable<TItem> expected)
        => _actual.Should().NotBeSameAs(expected);

    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    public AndConstraint<GenericCollectionAssertions<TItem>> EqualTo(IEnumerable<TItem> expected)
        => _actual.Should().BeEquivalentTo(expected);
}