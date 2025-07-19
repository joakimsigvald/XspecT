﻿using XspecT.Assert;
using XspecT.Test.AutoFixture;

namespace XspecT.Test.Assert.Continuations.Enumerable.IsEnumerable;

public class WhenDistinct : Spec
{
    [Fact]
    public void GivenEmpty_ThenDoesNotThrow()
    {
        Zero<int>().Is().Distinct();
        Specification.Is("Zero int is distinct");
    }

    [Fact]
    public void GivenOneItem_ThenDoesNotThrow()
    {
        One<int>().Is().Distinct();
        Specification.Is("One int is distinct");
    }

    [Fact]
    public void GivenTwoDifferentItems_ThenDoesNotThrow()
    {
        new int[] { 1, 2 }.Is().Distinct();
        Specification.Is("New int[] { 1, 2 } is distinct");
    }

    [Fact]
    public void GivenTwoIdenticalItems_ThenGetException()
    {
        int[] arr = [1, 1];
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Is().Distinct());
        ex.Message.Is($"Arr is distinct");
        ex.InnerException.Message.Is($"Expected arr to be distinct but found [1, 1]");
    }

    [Fact]
    public void GivenTwoEquivalentItems_ThenGetException()
    {
        MyModel[] arr = [new(), new()];
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => arr.Is().Distinct());
        ex.Message.Is($"Arr is distinct");
        ex.InnerException.Message.Is($"Expected arr to be distinct but found [MyModel {{ Id = 0, Name =  }}, MyModel {{ Id = 0, Name =  }}]");
    }
}