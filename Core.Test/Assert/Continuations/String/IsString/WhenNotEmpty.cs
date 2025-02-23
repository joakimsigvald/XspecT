using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNotEmpty : StringSpec
{
    [Fact]
    public void GivenNotEmpty_ThenDoesNotThrow()
        => "abc".Is().NotEmpty().And.NotEmpty();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GivenEmpty_ThenGetException(string actual)
    {
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().NotEmpty());
        ex.Message.Is("Actual is not empty");
        ex.InnerException.Message.Is($"Expected actual to be not empty but found {Describe(actual)}");
    }
}