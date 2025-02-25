using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsNullableStruct;

public class WhenNotValue : Spec
{
    [Theory]
    [InlineData(null, 1)]
    [InlineData(2, 1)]
    public void GivenNotSame_ThenDoesNotThrow(int? actualAmount, int expectedAmount)
    {
        Money? actual = actualAmount is null ? null : new Money(actualAmount.Value, "SEK");
        Money expected = new(expectedAmount, "SEK");
        actual.Is().Not(expected).And(expected).Is().NotNull();
    }

    [Fact]
    public void GivenSame_ThenGetException()
    {
        Money? actual = new(1, "SEK");
        Money expected = new(1, "SEK");
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Not(expected));
        ex.Message.Is("Actual is not expected");
        ex.InnerException.Message.Is($"Expected actual to be not {expected} but found {actual}");
    }
}