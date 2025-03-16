using XspecT.Assert;

namespace XspecT.Test.Assert.AssertionExtensions;

public class WhenIsNotArray : Spec<int[]>
{
    public WhenIsNotArray() => Given().Default(() => new int[] { 1, 2, 3 });

    [Fact] public void GivenNotSame_ThenDoesNotThrow() => When(_ => _.IsNot(null)).Then();

    [Fact]
    public void GivenFail_ThenGetException()
    {
        int[] arr = [1, 2];
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(()
            => When(_ => arr).Then().Result.IsNot(arr));
        ex.Message.Is(
            """
            Given new int[] { 1, 2, 3 } as default
            When arr
            Then Result is not arr
            """);
        ex.InnerException.Message.Is(
            "Expected Result to not be [1, 2] but found [1, 2]");
    }
}