﻿namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided double
/// </summary>
public record IsDouble : IsFractional<double, IsDouble>
{
    private protected override void AssertEqual(double expected, double precision)
        => Xunit.Assert.Equal(expected, Actual, precision);

    private protected override void AssertNotEqual(double expected, double precision)
        => Xunit.Assert.NotEqual(expected, Actual, precision);
}