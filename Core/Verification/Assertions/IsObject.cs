using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsObject
{
    private readonly object _actual;

    public IsObject(object actual) => _actual = actual;

    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.ObjectAssertions> Not(object expected)
        => _actual.Should().NotBeSameAs(expected);

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.ObjectAssertions> EqualTo(object expected)
        => _actual.Should().Be(expected);

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.ObjectAssertions> NotEqualTo(object expected)
        => _actual.Should().NotBe(expected);

    /// <summary>
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.ObjectAssertions> Like(object expected)
        => _actual.Should().BeEquivalentTo(expected);
}