using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsString
{
    private readonly string _actual;

    public IsString(string actual) => _actual = actual;

    /// <summary>
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.StringAssertions> IsLike(string expected)
        => _actual.Should().BeEquivalentTo(expected);
}