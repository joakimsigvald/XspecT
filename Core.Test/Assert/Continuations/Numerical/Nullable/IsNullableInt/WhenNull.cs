using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Continuations.Numerical.Nullable.IsNullableInt;

public class WhenNull : Spec
{
    [Fact] public void GivenNull_ThenDoesNotThrow() => ((int?)null).Is().Null();

    [Fact]
    public void GivenFail_ThenGetException()
    {
        int? x = 1;
        var ex = Xunit.Assert.Throws<XunitException>(() => x.Is().Null());
        ex.Message.Is("X is null");
        ex.InnerException.Message.Is("Expected x to be null but found 1");
    }
}