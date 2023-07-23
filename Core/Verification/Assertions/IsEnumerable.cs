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
}