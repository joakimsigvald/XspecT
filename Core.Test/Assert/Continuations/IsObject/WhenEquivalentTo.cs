﻿using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenEquivalentTo : Spec
{
    [Fact]
    public void GivenEquivalentTo_ThenDoesNotThrow()
        => new MyRecord("abc").Is().EquivalentTo(new MyOtherRecord("abc")).And.Not().Null();

    [Fact]
    public void GivenNotEquivalentTo_ThenGetException()
    {
        var actual = new MyRecord("abc");
        var expected = new MyOtherRecord("def");
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().EquivalentTo(expected));
        ex.Message.Is("Actual is equivalent to expected");
        ex.InnerException.Message.Is($"Expected actual to be equivalent to {expected} but found {actual}");
    }
}