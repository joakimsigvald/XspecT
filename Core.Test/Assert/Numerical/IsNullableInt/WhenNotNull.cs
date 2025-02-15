﻿using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Numerical.IsNullableInt;

public class WhenNotNull : Spec
{
    [Fact] public void GivenNotNull_ThenDoesNotThrow() => ((int?)1).Is().NotNull().And.LessThan(2);

    [Fact]
    public void GivenFail_ThenGetException()
    {
        int? x = null;
        var ex = Xunit.Assert.Throws<XunitException>(() => x.Is().NotNull());
        ex.Message.Is("X is not null");
        ex.InnerException.Message.Is("Expected x to be not null but found null");
    }
}