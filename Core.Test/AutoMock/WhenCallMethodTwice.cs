using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public abstract class WhenCallMethodTwice : Spec<InterfaceService, int>
{
    protected WhenCallMethodTwice() => When(_ => _.GetServiceValue() + _.GetServiceValue());

    //TODO add specification
    public class GivenOneMockedResponse : WhenCallMethodTwice
    {
        public GivenOneMockedResponse() => Given<IMyService>().That(_ => _.GetValue()).Returns(() => 1);
        [Fact] public void ThenReturnThatResponsTwice() => Then().Result.Is(2);
    }

    //TODO add specification
    public class GivenDifferentResponse : WhenCallMethodTwice
    {
        public GivenDifferentResponse()
            => Given<IMyService>().That(_ => _.GetValue()).First().Returns(() => 1).AndNext().Returns(() => 2);
        [Fact] public void ThenReturnDifferentResponses() => Then().Result.Is(3);
    }

    //TODO add specification
    public class GivenThrowsSecondTime : WhenCallMethodTwice
    {
        public GivenThrowsSecondTime()
            => Given<IMyService>().That(_ => _.GetValue()).First().Returns(() => 1).AndNext().Throws(An<ArgumentException>);
        [Fact] public void ThenThrows() => Then().Throws(The<ArgumentException>);
    }
}