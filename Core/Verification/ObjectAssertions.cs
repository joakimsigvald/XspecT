using FluentAssertions;

namespace XspecT.Verification;

public static class ObjectAssertions
{
    /// <summary>
    /// actual.Should().BeSameAs(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> Is(
        this object actual, object expected)
        => actual.Should().BeSameAs(expected);

    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> IsNot(
        this object actual, object expected)
        => actual.Should().NotBeSameAs(expected);

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> IsEqualTo(
        this object actual, object expected)
        => actual.Should().Be(expected);

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> IsNotEqualTo(
        this object actual, object expected)
        => actual.Should().NotBe(expected);

    /// <summary>
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> IsLike(
        this object actual, object expected)
        => actual.Should().BeEquivalentTo(expected);
}