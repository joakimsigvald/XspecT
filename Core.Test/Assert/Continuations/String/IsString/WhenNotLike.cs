using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNotLike : StringSpec
{
    [Theory]
    [InlineData(null, "")]
    [InlineData("", null)]
    [InlineData("", "abc")]
    [InlineData("abc", "abcd")]
    public void GivenNotLikeString_ThenDoesNotThrow(string actual, string expected)
        => actual.Is().NotLike(expected).And.Does().NotContain("XXX");

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("abc", "abc")]
    [InlineData("abc", "ABC")]
    public void GivenLikeString_ThenGetException(string actual, string expected)
    {
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().NotLike(expected));
        ex.Message.Is("Actual is not like expected");
        ex.InnerException.Message.Is($"Expected actual to be not like {Describe(expected)} but found {Describe(actual)}");
    }
}