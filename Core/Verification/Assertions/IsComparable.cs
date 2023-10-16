using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsComparable<TActual> : Constraint<IsComparable<TActual>>
    where TActual : IComparable<TActual>
{
    private readonly TActual _actual;

    public IsComparable(TActual actual) => _actual = actual;

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<IsComparable<TActual>> GreaterThan(TActual expected)
    {
        _actual.Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<IsComparable<TActual>> LessThan(TActual expected)
    {
        _actual.Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<IsComparable<TActual>> NotGreaterThan(TActual expected)
    {
        _actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<IsComparable<TActual>> NotLessThan(TActual expected)
    {
        _actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }
}
