using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsComparable<TActual> : Constraint<IsComparable<TActual>, TActual>
    where TActual : IComparable<TActual>
{
    public IsComparable(TActual actual) : base(actual) { }

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<IsComparable<TActual>> GreaterThan(TActual expected)
    {
        Actual.Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<IsComparable<TActual>> LessThan(TActual expected)
    {
        Actual.Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<IsComparable<TActual>> NotGreaterThan(TActual expected)
    {
        Actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<IsComparable<TActual>> NotLessThan(TActual expected)
    {
        Actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }
}
