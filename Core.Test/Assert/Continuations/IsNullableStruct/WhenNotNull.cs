using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsNullableStruct;

public class WhenNotNull : Spec
{
    [Fact] public void GivenNotNull_ThenDoesNotThrow() => ((Money?)new Money()).Is().Not().Null().And.Not().Null();

    [Fact]
    public void GivenNull_ThenGetException()
    {
        Money? actual = null;
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Not().Null());
        ex.Message.Is("Actual is not null");
        ex.HasInnerMessage("Expected actual to not be null but found null");
    }
}