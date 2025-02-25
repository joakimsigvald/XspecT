using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsNullableStruct;

public class WhenNotNullableValue : Spec
{
    [Theory]
    [InlineData(null, 1)]
    [InlineData(1, null)]
    [InlineData(2, 1)]
    public void GivenNotSame_ThenDoesNotThrow(int? actualAmount, int? expectedAmount)
    {
        Money? actual = actualAmount is null ? null : new Money(actualAmount.Value, "SEK");
        Money? expected = expectedAmount is null ? null : new Money(expectedAmount.Value, "SEK");
        actual.Is().Not(expected).And.Is().Not(expected);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(1, 1)]
    public void GivenSame_ThenGetException(int? actualAmount, int? expectedAmount)
    {
        Money? actual = actualAmount is null ? null : new Money(actualAmount.Value, "SEK");
        Money? expected = expectedAmount is null ? null : new Money(expectedAmount.Value, "SEK");
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Not(expected));
        ex.Message.Is("Actual is not expected");
        string expectedStr = expected?.ToString() ?? "null";
        ex.InnerException.Message.Is($"Expected actual to be not {expectedStr} but found {expectedStr}");
    }
}