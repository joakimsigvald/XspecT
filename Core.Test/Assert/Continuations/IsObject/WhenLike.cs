﻿using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenLike : Spec
{
    [Fact]
    public void GivenLike_ThenDoesNotThrow()
        => new MyRecord("abc").Is().Like(new MyOtherRecord("abc")).And.Not().Null();

    [Fact]
    public void GivenNotLike_ThenGetException()
    {
        var actual = new MyRecord("abc");
        var expected = new MyOtherRecord("def");
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Like(expected));
        ex.Message.Is("Actual is like expected");
        ex.InnerException.Message.Is($"Expected actual to be like {expected} but found {actual}");
    }
}