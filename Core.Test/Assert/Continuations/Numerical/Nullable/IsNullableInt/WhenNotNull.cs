using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Numerical.Nullable.IsNullableInt;

public class WhenNotNull : Spec
{
    [Fact] public void GivenNotNull_ThenDoesNotThrow() => ((int?)1).Is().Not().Null().And.LessThan(2);

    [Fact]
    public void GivenFail_ThenGetException()
    {
        int? x = null;
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => x.Is().Not().Null());
        ex.Message.Is("X is not null");
        ex.InnerException.Message.Is("Expected x to not be null but found null");
    }
}