using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsNullableStruct;

public class WhenNull : Spec
{
    [Fact] public void GivenNull_ThenDoesNotThrow() => ((Money?)null).Is().Null();

    [Fact]
    public void GivenNotNull_ThenGetException()
    {
        Money? actual = new();
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Null());
        ex.Message.Is("Actual is null");
        ex.HasInnerMessage("Expected actual to be null but found Money { Amount = 0, Currency =  }");
    }
}