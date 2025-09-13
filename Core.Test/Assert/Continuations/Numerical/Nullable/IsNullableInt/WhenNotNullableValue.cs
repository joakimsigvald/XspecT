﻿using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Numerical.Nullable.IsNullableInt;

public class WhenNotNullableValue : Spec
{
    [Fact] public void GivenDifferent_ThenDoesNotThrow() => ((int?)1).Is().Not((int?)2).And.Not((int?)0);

    [Fact]
    public void GivenFail_ThenGetException()
    {
        int? x = 1;
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => x.Is().Not(x));
        ex.Message.Is("X is not x");
        ex.HasInnerMessage("Expected x to be not 1 but found 1");
    }
}