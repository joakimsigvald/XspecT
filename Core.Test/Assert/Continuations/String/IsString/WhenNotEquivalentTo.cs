using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNotEquivalentTo : StringSpec
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("", null)]
    [InlineData("", "abc")]
    [InlineData("abc", "abcd")]
    public void GivenNotEquivalentToString_ThenDoesNotThrow(string actual, string expected)
        => actual.Is().NotEquivalentTo(expected).And.NotEquivalentTo(expected);

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("abc", "abc")]
    [InlineData("abc", "ABC")]
    public void GivenEquivalentToString_ThenGetException(string actual, string expected)
    {
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().NotEquivalentTo(expected));
        ex.Message.Is("Actual is not equivalent to expected");
        ex.InnerException.Message.Is($"Expected actual to be not equivalent to {Describe(expected)} but found {Describe(actual)}");
    }
}