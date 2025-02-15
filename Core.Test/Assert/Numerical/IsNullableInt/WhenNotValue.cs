﻿using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Numerical.IsNullableInt;

public class WhenNotValue : Spec
{
    [Fact] public void GivenDifferent_ThenDoesNotThrow() => ((int?)1).Is().Not(2).And.GreaterThan(0);

    [Fact]
    public void GivenFail_ThenGetException()
    {
        int? x = 1;
        var ex = Xunit.Assert.Throws<XunitException>(() => x.Is().Not(x));
        ex.Message.Is("X is not x");
        ex.InnerException.Message.Is("Expected x to be not 1 but found 1");
    }
}