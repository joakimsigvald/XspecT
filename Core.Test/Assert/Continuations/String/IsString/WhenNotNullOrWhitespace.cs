using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNotNullOrWhitespace : StringSpec
{
    [Fact]
    public void GivenNotNullOrWhitespace_ThenDoesNotThrow()
        => "abc".Is().NotNullOrWhitespace().And.NotNullOrWhitespace();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GivenNullOrWhitespace_ThenGetException(string actual)
    {
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().NotNullOrWhitespace());
        ex.Message.Is("Actual is not null or whitespace");
        ex.InnerException.Message.Is($"Expected actual to be not null or whitespace but found {Describe(actual)}");
    }
}