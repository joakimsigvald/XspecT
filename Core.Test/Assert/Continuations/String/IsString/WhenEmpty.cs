using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenEmpty : StringSpec
{
    [Fact]
    public void GivenEmpty_ThenDoesNotThrow()
        => "".Is().Empty().And.Does().Contain("");

    [Theory]
    [InlineData(null)]
    [InlineData("abc")]
    public void GivenNotEmpty_ThenGetException(string actual)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Empty());
        ex.Message.Is("Actual is empty");
        ex.InnerException.Message.Is($"Expected actual to be empty but found {Describe(actual)}");
    }
}