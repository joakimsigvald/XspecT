﻿namespace XspecT.Verification.Assertions.Numerical;

public abstract class IsNullableNumerical<TActual, TConstrain> : Constraint<TConstrain, TActual?>
    where TActual : struct, IComparable<TActual>
    where TConstrain : IsNullableNumerical<TActual, TConstrain>
{
    internal IsNullableNumerical(TActual? actual) : base(actual) { }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TConstrain> Null()
    {
        Should().BeNull();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TConstrain> NotNull()
    {
        Should().NotBeNull();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TConstrain> Not(TActual? expected)
    {
        Should().NotBe(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<TConstrain> GreaterThan(TActual expected)
    {
        Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<TConstrain> LessThan(TActual expected)
    {
        Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TConstrain> NotGreaterThan(TActual expected)
    {
        Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TConstrain> NotLessThan(TActual expected)
    {
        Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }

    protected abstract FluentAssertions.Numeric.NullableNumericAssertions<TActual> Should();
}