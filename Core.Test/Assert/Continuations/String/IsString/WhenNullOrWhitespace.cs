﻿using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNullOrWhitespace : StringSpec
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GivenNullOrWhitespace_ThenDoesNotThrow(string actual)
        => actual.Is().NullOrWhitespace().And.NullOrWhitespace();

    [Fact]
    public void GivenNotNullOrWhitespace_ThenGetException()
    {
        var str = "abc";
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => str.Is().NullOrWhitespace());
        ex.Message.Is("Str is null or whitespace");
        ex.InnerException.Message.Is($"Expected str to be null or whitespace but found {Describe(str)}");
    }
}