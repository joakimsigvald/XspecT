﻿using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Time.IsNullableDateTime;

public class WhenNotAfter : Spec
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GivenNotAfter_ThenDoesNotThrow(int days)
        => A<DateTime?>().Is().Not().After(The<DateTime?>().Value.AddDays(days))
        .And.CloseTo(The<DateTime?>().Value, TimeSpan.Zero);

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime?>();
        var b = a.Value.AddDays(-1);
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().Not().After(b));
        ex.Message.Is("A is not after b");
        ex.InnerException.Message.Is($"Expected a to not occur after {b} but found {a}");
    }
}