using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNot : StringSpec
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("", null)]
    [InlineData("", "abc")]
    [InlineData("abc", "abcd")]
    [InlineData("abc", "ABC")]
    public void GivenNotSameString_ThenDoesNotThrow(string actual, string expected)
        => actual.Is().Not(expected).And.Does().Not().Contain("XXX");

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("abc", "abc")]
    public void GivenSameString_ThenGetException(string actual, string expected)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Not(expected));
        ex.Message.Is("Actual is not expected");
        ex.HasInnerMessage($"Expected actual to be not {Describe(expected)} but found {Describe(actual)}");
    }
}