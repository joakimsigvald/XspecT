using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsString
{
    private readonly string _actual;

    public IsString(string actual) => _actual = actual;

    /// <summary>
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.StringAssertions> Like(string expected)
        => _actual.Should().BeEquivalentTo(expected);

    /// <summary>
    /// actual.Should().BeNull(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.StringAssertions> Null()
        => _actual.Should().BeNull();

    /// <summary>
    /// actual.Should().NotBeNull(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.StringAssertions> NotNull()
        => _actual.Should().NotBeNull();

    /// <summary>
    /// actual.Should().BeNullOrEmpty(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.StringAssertions> NullOrEmpty()
        => _actual.Should().BeNullOrEmpty();

    /// <summary>
    /// actual.Should().NotBeNullOrEmpty(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.StringAssertions> NotNullOrEmpty()
        => _actual.Should().NotBeNullOrEmpty();

    /// <summary>
    /// actual.Should().BeNullOrWhiteSpace(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.StringAssertions> NullOrWhitespace()
        => _actual.Should().BeNullOrWhiteSpace();

    /// <summary>
    /// actual.Should().NotBeNullOrWhiteSpace(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.StringAssertions> NotNullOrWhitespace()
        => _actual.Should().NotBeNullOrWhiteSpace();
}