using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Enumerable.DoesEnumerable;

public class WhenNotContain : EnumerableSpec
{
    [Theory]
    [InlineData("", 'a')]
    [InlineData("abc", 'd')]
    [InlineData("abc", 'A')]
    public void GivenListNotContainItem_ThenDoesNotThrow(string actual, char expected)
        => actual.ToList().Does().Not().Contain(expected).And.Is().Not().Null();

    [Theory]
    [InlineData("abc", 'a')]
    public void GivenListContainItem_ThenGetException(string actual, char expected)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.ToList().Does().Not().Contain(expected));
        ex.Message.Is("Actual.ToList() does not contain expected");
        ex.HasInnerMessage($"Expected actual.ToList() to not contain {expected} but found {Describe(actual)}");
    }
}