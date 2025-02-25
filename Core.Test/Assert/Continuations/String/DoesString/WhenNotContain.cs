using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.DoesString;

public class WhenNotContain : StringSpec
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("", "abc")]
    [InlineData("abc", "abcd")]
    [InlineData("Abc", "abc")]
    public void GivenNotContainString_ThenDoesNotThrow(string actual, string expected) 
        => actual.Does().NotContain(expected).And.NotContain(expected);

    [Theory]
    [InlineData(null, null)]
    [InlineData("", null)]
    [InlineData("", "")]
    [InlineData("abc", "abc")]
    [InlineData("xabcyz", "abc")]
    public void GivenContainString_ThenGetException(string actual, string expected)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Does().NotContain(expected));
        ex.Message.Is("Actual does not contain expected");
        ex.InnerException.Message.Is($"Expected actual to not contain {Describe(expected)} but found {Describe(actual)}");
    }
}