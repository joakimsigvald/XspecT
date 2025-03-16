using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNotEmpty : StringSpec
{
    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    public void GivenNotEmpty_ThenDoesNotThrow(string actual)
        => actual.Is().Not().Empty().And.Not().Empty();

    [Theory]
    [InlineData("")]
    public void GivenEmpty_ThenGetException(string actual)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Not().Empty());
        ex.Message.Is("Actual is not empty");
        ex.InnerException.Message.Is($"Expected actual to not be empty but found {Describe(actual)}");
    }
}