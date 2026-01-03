using XspecT.Assert;
using XspecT.Internal.Specification;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Collections;

public class Distinct : Spec
{
    [Theory]
    [InlineData(1, 2)]
    public void GivenDistinct(params int[] numbers)
    {
        numbers.Is().Distinct();
        Specification.Is($"Numbers is distinct");
    }

    [Theory]
    [InlineData(1, 1)]
    public void GivenNotDistinct(params int[] numbers)
    {
        numbers.Is().not.Distinct();
        Specification.Is($"Numbers is not distinct");
    }

    [Theory]
    [InlineData(1, 1)]
    public void GivenDistinctFail(params int[] numbers)
    {
        var ex = Xunit.Assert.Throws<XunitException>(() => numbers.Is().Distinct());
        ex.Message.Is($"Expected numbers to be distinct but found {numbers.ParseValue()}");
    }

    [Theory]
    [InlineData(1, 2)]
    public void GivenNotDistinctFail(params int[] numbers)
    {
        var ex = Xunit.Assert.Throws<XunitException>(() => numbers.Is().not.Distinct());
        ex.Message.Is($"Expected numbers to not be distinct but found {numbers.ParseValue()}");
    }
}