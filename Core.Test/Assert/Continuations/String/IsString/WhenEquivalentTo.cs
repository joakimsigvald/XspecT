using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenEquivalentTo : StringSpec
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("abc", "abc")]
    [InlineData("abc", "ABC")]
    public void GivenEquivalentToString_ThenDoesNotThrow(string actual, string expected)
        => actual.Is().EquivalentTo(expected).And.Does().NotContain("XXX");

    [Theory]
    [InlineData(null, "")]
    [InlineData("", null)]
    [InlineData("", "abc")]
    [InlineData("abc", "abcd")]
    public void GivenNotEquivalentToString_ThenGetException(string actual, string expected)
    {
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().EquivalentTo(expected));
        ex.Message.Is("Actual is equivalent to expected");
        ex.InnerException.Message.Is($"Expected actual to be equivalent to {Describe(expected)} but found {Describe(actual)}");
    }
}