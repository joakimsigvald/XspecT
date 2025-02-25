using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNotNullOrEmpty : StringSpec
{
    [Fact]
    public void GivenNotNullOrEmpty_ThenDoesNotThrow()
        => "abc".Is().NotNullOrEmpty().And.Does().Contain("a");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GivenNullOrEmpty_ThenGetException(string actual)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().NotNullOrEmpty());
        ex.Message.Is("Actual is not null or empty");
        ex.InnerException.Message.Is($"Expected actual to be not null or empty but found {Describe(actual)}");
    }
}