﻿namespace XspecT.Test.AutoMock;

public class WhenMockInt : Spec<StaticIntService, int>
{
    public WhenMockInt() => Given(An<int>).When(_ => _.GetValue());
    public class UsingAValue : WhenMockInt
    {
        [Fact]
        public void Then_It_Has_theValue()
        {
            Then().Result.Is(The<int>());
            Description.Is(
                """
                Given an int
                When _.GetValue()
                Then Result is the int
                """);
        }
    }

    public class GivenItWasProvided : WhenMockInt
    {
        [Theory]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void Then_It_Has_ProvidedValue(int value)
        {
            Given(value).Then().Result.Is(value);
            Description.Is(
                """
                Given value
                 and an int
                When _.GetValue()
                Then Result is value
                """);
        }
    }
}