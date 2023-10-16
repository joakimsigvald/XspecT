using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsNumerical<TActual> : Constraint<IsNumerical<TActual>, TActual>
    where TActual : struct, IComparable<TActual>
{
    public IsNumerical(TActual actual) : base(actual) { }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<IsNumerical<TActual>> Not(TActual expected)
    {
        Actual.Should().NotBe(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<IsNumerical<TActual>> GreaterThan(TActual expected)
    {
        Actual.Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<IsNumerical<TActual>> LessThan(TActual expected)
    {
        Actual.Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<IsNumerical<TActual>> NotGreaterThan(TActual expected)
    {
        Actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<IsNumerical<TActual>> NotLessThan(TActual expected)
    {
        Actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }
}