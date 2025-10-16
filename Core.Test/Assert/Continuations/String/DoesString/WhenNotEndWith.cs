using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.DoesString;

public class WhenNotEndWith : StringSpec
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("", "abc")]
    [InlineData("abc", "ab")]
    [InlineData("abc", "Bc")]
    public void GivenNotEndWithString_ThenDoesNotThrow(string? actual, string expected) 
        => actual.Does().Not().EndWith(expected).And.Not().EndWith(expected);

    [Theory]
    [InlineData("", "")]
    [InlineData("abc", "abc")]
    [InlineData("xabcyz", "cyz")]
    public void GivenEndWithString_ThenGetException(string actual, string expected)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Does().Not().EndWith(expected));
        ex.Message.Is("Actual does not end with expected");
        ex.HasInnerMessage($"Expected actual to not end with {Describe(expected)} but found {Describe(actual)}");
    }
}