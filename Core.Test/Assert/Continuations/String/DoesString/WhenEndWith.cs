using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.DoesString;

public class WhenEndWith : StringSpec
{
    [Theory]
    [InlineData("", "")]
    [InlineData("abc", "abc")]
    [InlineData("xabcyz", "cyz")]
    public void GivenEndWithString_ThenDoesNotThrow(string actual, string expected) 
        => actual.Does().EndWith(expected).And.EndWith(expected);

    [Theory]
    [InlineData(null, "")]
    [InlineData("", "abc")]
    [InlineData("abc", "ab")]
    [InlineData("abc", "Bc")]
    public void GivenNotEndWithString_ThenGetException(string? actual, string expected)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Does().EndWith(expected));
        ex.Message.Is("Actual ends with expected");
        ex.HasInnerMessage($"Expected actual to end with {Describe(expected)} but found {Describe(actual)}");
    }
}