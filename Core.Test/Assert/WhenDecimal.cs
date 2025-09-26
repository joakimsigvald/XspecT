﻿using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenDecimal : Spec<decimal>
{
    [Fact] public void IsAroundSame() => When(_ => The(_)).Then().Result.Is().Around(The<decimal>(), 1);
    [Fact] public void IsAroundUpperBound() => When(_ => 1).Then().Result.Is().Around(2, 1);
    [Fact] public void IsAroundLowerBound() => When(_ => 1).Then().Result.Is().Around(0, 1);
    [Fact] public void IsNotAroundAboveUpperBound() => When(_ => 1).Then().Result.Is().Not().Around(2.001m, 1);
    [Fact] public void IsNotAroundBelowLowerBound() => When(_ => 1).Then().Result.Is().Not().Around(-0.001m, 1);
    [Fact] public void IsNotAroundUpperBoundFail() 
        => Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => When(_ => 1).Then().Result.Is().Not().Around(2, 1));
    [Fact] public void IsNotAroundLowerBoundFail() 
        => Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => When(_ => 1).Then().Result.Is().Not().Around(0, 1));
    [Fact] public void IsAroundAboveUpperBoundFail() 
        => Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => When(_ => 1).Then().Result.Is().Around(2.001m, 1));
    [Fact] public void IsAroundBelowLowerBoundFail() 
        => Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => When(_ => 1).Then().Result.Is().Around(-0.001m, 1));
}