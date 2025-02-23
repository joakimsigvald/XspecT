using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNotNull : StringSpec
{
    [Fact]
    public void GivenNotNull_ThenDoesNotThrow()
        => "".Is().NotNull().And.NotNull();

    [Fact]
    public void GivenNull_ThenGetException()
    {
        string str = null;
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => str.Is().NotNull());
        ex.Message.Is("Str is not null");
        ex.InnerException.Message.Is($"Expected str to be not null but found {Describe(str)}");
    }
}