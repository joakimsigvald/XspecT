﻿using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenFloat : Spec<float>
{
    [Fact] public void IsAroundSame() => When(_ => A(_)).Then().Result.Is().Around(The<float>(), 1);
    [Fact] public void IsAroundUpperBound() => When(_ => 1).Then().Result.Is().Around(2, 1);
    [Fact] public void IsAroundLowerBound() => When(_ => 1).Then().Result.Is().Around(0, 1);
    [Fact] public void IsNotAroundAboveUpperBound() => When(_ => 1).Then().Result.Is().NotAround(2.001f, 1);
    [Fact] public void IsNotAroundBelowLowerBound() => When(_ => 1).Then().Result.Is().NotAround(-0.001f, 1);
    [Fact] public void IsNotAroundUpperBoundFail() 
        => Xunit.Assert.Throws<AssertionFailed>(() => When(_ => 1).Then().Result.Is().NotAround(2, 1));
    [Fact] public void IsNotAroundLowerBoundFail() 
        => Xunit.Assert.Throws<AssertionFailed>(() => When(_ => 1).Then().Result.Is().NotAround(0, 1));
    [Fact] public void IsAroundAboveUpperBoundFail() 
        => Xunit.Assert.Throws<AssertionFailed>(() => When(_ => 1).Then().Result.Is().Around(2.001f, 1));
    [Fact] public void IsAroundBelowLowerBoundFail() 
        => Xunit.Assert.Throws<AssertionFailed>(() => When(_ => 1).Then().Result.Is().Around(-0.001f, 1));
}