﻿using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Numerical.Fractional.IsDouble;

public class WhenNotAround : Spec
{
    [Theory]
    [InlineData(1, 2, 0)]
    [InlineData(1, 3, 1.99)]
    public void GivenNotAround_ThenDoesNotThrow(double a, double b, double precision)
        => a.Is().Not().Around(b, precision);

    [Theory]
    [InlineData(1, 1, 0)]
    [InlineData(2, 3, 1)]
    public void GivenFail_ThenGetException(double a, double b, double precision)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().Not().Around(b, precision));
        ex.Message.Is($"A is not around b");
        ex.InnerException.Message.Is($"Expected a to not be around {b} but found {a}");
    }
}