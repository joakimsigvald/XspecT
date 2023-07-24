using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsObject
{
    private readonly object _actual;

    public IsObject(object actual) => _actual = actual;

    /// <summary>
    /// Should().NotBeSameAs(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.ObjectAssertions> Not(object expected)
        => _actual.Should().NotBeSameAs(expected);

    /// <summary>
    /// Should().BeNull()
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.ObjectAssertions> Null()
        => _actual.Should().BeNull();

    /// <summary>
    /// Should().NotBeNull()
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.ObjectAssertions> NotNull()
        => _actual.Should().NotBeNull();

    /// <summary>
    /// Should().Be(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.ObjectAssertions> EqualTo(object expected)
        => _actual.Should().Be(expected);

    /// <summary>
    /// Should().NotBe(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.ObjectAssertions> NotEqualTo(object expected)
        => _actual.Should().NotBe(expected);

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.ObjectAssertions> Like(object expected)
        => _actual.Should().BeEquivalentTo(expected);
}