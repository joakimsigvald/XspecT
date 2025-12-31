using XspecT.Assert;
using XspecT.Internal.Specification;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Collections;

public class Count : Spec
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 1, 3)]
    public void GivenCorrectCount(int count, params int[] numbers) => numbers.Has().Count(count);

    [Theory]
    [InlineData(2, "Expected numbers to have count 2 but found 1: [1]", 1)]
    [InlineData(1, "Expected numbers to have count 1 but found 2: [1, 3]", 1, 3)]
    [InlineData(3, "Expected numbers to have count 3 but found 6: [1, 2, 3, 4, ...]", 1, 2, 3, 4, 5, 6)]
    public void GivenWrongCount(int count, string errorMessage, params int[] numbers)
    {
        var ex = Xunit.Assert.Throws<XunitException>(() => numbers.Has().Count(count));
        ex.Message.Is(errorMessage);
    }

    [Theory]
    [InlineData(2, 1)]
    [InlineData(1, 1, 3)]
    public void GivenNotCount(int count, params int[] numbers) => numbers.Has().Not().Count(count);

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 1, 3)]
    public void GivenAtLeast(int count, params int[] numbers)
    {
        numbers.Has().CountAtLeast(count);
        Specification.Is(
            $"Numbers has count at least 'count' = {count}");
    }

    [Theory]
    [InlineData(2, 1)]
    [InlineData(2, 1, 3)]
    public void GivenAtMost(int count, params int[] numbers)
    {
        numbers.Has().CountAtMost(count);
        Specification.Is(
            $"Numbers has count at most 'count' = {count}");
    }

    [Theory]
    [InlineData(1, 2, 1)]
    [InlineData(1, 2, 1, 3)]
    public void GivenInRange(int from, int to, params int[] numbers)
    {
        numbers.Has().CountInRange(from, to);
        Specification.Is(
            $"Numbers has count in range ['from', 'to'] = [{from}, {to}]");
    }

    [Theory]
    [InlineData(11, 12, 1)]
    [InlineData(11, 12, 1, 3)]
    public void GivenNotInRange(int from, int to, params int[] numbers)
    {
        numbers.Has().Not().CountInRange(from, to);
        Specification.Is(
            $"Numbers has not count in range ['from', 'to'] = [{from}, {to}]");
    }

    [Fact]
    public void GivenInvalidRange()
    {
        var ex = Xunit.Assert.Throws<SetupFailed>(() => Two<int>().Has().CountInRange(3, 2));
        ex.Message.Is("Given range must be in ascending order");
    }

    [Fact]
    public void GivenAtLeastFail()
    {
        var ex = Xunit.Assert.Throws<XunitException>(() => Two<int>().Has().CountAtLeast(3));
        ex.Message.Is($"Expected two int to have count at least 3 but found {Two<int>().ParseValue()}");
    }
}