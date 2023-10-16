using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsNumerical<TActual> : Constraint<IsNumerical<TActual>>
    where TActual : struct, IComparable<TActual>
{
    private readonly TActual _actual;

    public IsNumerical(TActual actual) => _actual = actual;

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<IsNumerical<TActual>> Not(TActual expected)
    {
        _actual.Should().NotBe(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<IsNumerical<TActual>> GreaterThan(TActual expected)
    {
        _actual.Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<IsNumerical<TActual>> LessThan(TActual expected)
    {
        _actual.Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<IsNumerical<TActual>> NotGreaterThan(TActual expected)
    {
        _actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<IsNumerical<TActual>> NotLessThan(TActual expected)
    {
        _actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }
}