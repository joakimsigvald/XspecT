using FluentAssertions;

namespace XspecT.Verification;

public static class ObjectAssertionsObsolete
{
    /// <summary>
    /// actual.Should().NotBeSameAs(expected)
    /// </summary>
    [Obsolete("Replaced by Is().Not()")]
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> IsNot(
        this object actual, object expected)
        => actual.Should().NotBeSameAs(expected);

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    [Obsolete("Replaced by Is().EqualTo()")]
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> IsEqualTo(
        this object actual, object expected)
        => actual.Should().Be(expected);

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    [Obsolete("Replaced by Is().NotEqualTo()")]
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> IsNotEqualTo(
        this object actual, object expected)
        => actual.Should().NotBe(expected);

    /// <summary>
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    [Obsolete("Replaced by Is().Like()")]
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> IsLike(
        this object actual, object expected)
        => actual.Should().BeEquivalentTo(expected);
}