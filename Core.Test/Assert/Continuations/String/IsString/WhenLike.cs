using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenLike : StringSpec
{
    [Theory]
    [InlineData("", "")]
    [InlineData("abc", "abc")]
    [InlineData("abc", "ABC")]
    public void GivenLikeString_ThenDoesNotThrow(string actual, string expected)
        => actual.Is().Like(expected).And.Does().Not().Contain("XXX");

    [Theory]
    [InlineData("", "abc")]
    [InlineData("abc", "abcd")]
    public void GivenNotLikeString_ThenGetException(string actual, string expected)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Like(expected));
        ex.Message.Is("Actual is like expected");
        ex.HasInnerMessage($"Expected actual to be like {Describe(expected)} but found {Describe(actual)}");
    }
}