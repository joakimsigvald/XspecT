using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsBool
{
    private readonly bool _actual;

    public IsBool(bool actual) => _actual = actual;
    /// <summary>
    /// actual.Should().BeTrue()
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.BooleanAssertions> True()
        => _actual.Should().BeTrue();

    /// <summary>
    /// actual.Should().BeFalse()
    /// </summary>
    public AndConstraint<FluentAssertions.Primitives.BooleanAssertions> False()
        => _actual.Should().BeFalse();
}