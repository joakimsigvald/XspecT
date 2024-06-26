﻿using XspecT.Assert;
using Xunit;

namespace XspecT.Test.Verification;

public class WhenNullableTimeSpan : Spec<TimeSpan?>
{
    [Fact] 
    public void IsSame() 
        => When(A).Then().Result.Is(The<TimeSpan?>())
        .And.NotNull();

    [Fact]
    public void IsSameNotNullable()
        => When(A).Then().Result.Is(The<TimeSpan?>().Value)
        .And.CloseTo(The<TimeSpan?>().Value, TimeSpan.Zero);

    [Fact] public void IsNull() => When(_ => (TimeSpan?)null).Then().Result.Is().Null();
}