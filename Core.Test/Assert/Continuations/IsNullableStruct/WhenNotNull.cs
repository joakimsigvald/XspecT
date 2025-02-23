using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsNullableStruct;

public class WhenNotNull : Spec
{
    [Fact] public void GivenNotNull_ThenDoesNotThrow() => ((Money?)new Money()).Is().NotNull().And.NotNull();

    [Fact]
    public void GivenNull_ThenGetException()
    {
        Money? actual = null;
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().NotNull());
        ex.Message.Is("Actual is not null");
        ex.InnerException.Message.Is("Expected actual to be not null but found null");
    }
}