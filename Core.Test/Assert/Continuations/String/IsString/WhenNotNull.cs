using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNotNull : StringSpec
{
    [Fact]
    public void GivenNotNull_ThenDoesNotThrow()
        => "".Is().Not().Null().And.Not().Null();

    [Fact]
    public void GivenNull_ThenGetException()
    {
        string str = null;
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => str.Is().Not().Null());
        ex.Message.Is("Str is not null");
        ex.InnerException.Message.Is($"Expected str to not be null but found {Describe(str)}");
    }
}