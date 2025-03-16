using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNotNullOrWhitespace : StringSpec
{
    [Fact]
    public void GivenNotNullOrWhitespace_ThenDoesNotThrow()
        => "abc".Is().Not().NullOrWhitespace().And.Not().NullOrWhitespace();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GivenNullOrWhitespace_ThenGetException(string actual)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Not().NullOrWhitespace());
        ex.Message.Is("Actual is not null or whitespace");
        ex.InnerException.Message.Is($"Expected actual to not be null or whitespace but found {Describe(actual)}");
    }
}