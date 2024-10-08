﻿using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public abstract class WhenCallMethodTwice : Spec<InterfaceService, int>
{
    protected WhenCallMethodTwice() => When(_ => _.GetServiceValue() + _.GetServiceValue());

    public class GivenOneMockedResponse : WhenCallMethodTwice
    {
        public GivenOneMockedResponse() => Given<IMyService>().That(_ => _.GetValue()).Returns(() => 1);
        [Fact] public void ThenReturnThatResponsTwice() => Then().Result.Is(2);
    }

    public class GivenAndNextWithoutFirst : WhenCallMethodTwice
    {
        [Fact]
        public void ThenThrowSetupFailed()
        {
            var ex = Xunit.Assert.Throws<SetupFailed>(
                () => Given<IMyService>().That(_ => _.GetValue()).Returns(() => 1).AndNext().Returns(() => 1)
                .Then().Result.Is(2));
            ex.Message.Does().Contain("First");
        }
    }

    public class GivenDifferentResponse : WhenCallMethodTwice
    {
        public GivenDifferentResponse()
            => Given<IMyService>().That(_ => _.GetValue()).First().Returns(() => 1).AndNext().Returns(() => 2);
        [Fact] public void ThenReturnDifferentResponses() => Then().Result.Is(3);
    }

    public class GivenThrowsSecondTime : WhenCallMethodTwice
    {
        public GivenThrowsSecondTime()
            => Given<IMyService>().That(_ => _.GetValue()).First().Returns(() => 1).AndNext().Throws(An<ArgumentException>);
        [Fact] public void ThenThrows() => Then().Throws(The<ArgumentException>);
    }
}