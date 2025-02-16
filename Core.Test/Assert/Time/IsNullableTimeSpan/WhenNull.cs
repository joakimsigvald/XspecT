﻿using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Time.IsNullableTimeSpan;

public class WhenNull : Spec
{
    [Fact] public void GivenNull_ThenDoesNotThrow() => ((TimeSpan?)null).Is().Null();

    [Fact]
    public void GivenFail_ThenGetException()
    {
        TimeSpan? x = TimeSpan.Zero;
        var ex = Xunit.Assert.Throws<XunitException>(() => x.Is().Null());
        ex.Message.Is("X is null");
        ex.InnerException.Message.Is("Expected x to be null but found 00:00:00");
    }
}