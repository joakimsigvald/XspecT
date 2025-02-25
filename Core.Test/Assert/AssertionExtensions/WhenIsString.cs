using XspecT.Assert;

namespace XspecT.Test.Assert.AssertionExtensions;

public class WhenIsString : Spec<string>
{
    [Fact] public void GivenSame_ThenDoesNotThrow() => When(_ => _.Is(_)).Then();

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => Given("abcd").When(_ => _).Then().Result.Is("abce"));
        ex.InnerException.Message.Is(
            """
            Assert.Equal() Failure: Strings differ
                          ↓ (pos 3)
            Expected: "abce"
            Actual:   "abcd"
                          ↑ (pos 3)
            """);
    }
}
