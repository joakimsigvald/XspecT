using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNullOrEmpty : StringSpec
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GivenNullOrEmpty_ThenDoesNotThrow(string actual)
        => actual.Is().NullOrEmpty().And.NullOrEmpty();

    [Fact]
    public void GivenNotNullOrEmpty_ThenGetException()
    {
        var str = "abc";
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => str.Is().NullOrEmpty());
        ex.HasMessage($"Expected str to be null or empty but found {Describe(str)}", "Str is null or empty");
    }
}