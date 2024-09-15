using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public abstract class WhenCallAsyncMethodTwice : Spec<InterfaceService, int>
{
    protected WhenCallAsyncMethodTwice() => When(async _ => await _.GetServiceValueAsync() + await _.GetServiceValueAsync());

    public class GivenOneMockedResponse : WhenCallAsyncMethodTwice
    {
        public GivenOneMockedResponse() => Given<IMyService>().That(_ => _.GetValueAsync()).Returns(() => 1);
        [Fact]
        public void ThenReturnThatResponsTwice()
        {
            Then().Result.Is(2);
            Specification.Is(
                """
                Given IMyService.GetValueAsync() returns 1
                When async _ => await _.GetServiceValueAsync() + await _.GetServiceValueAsync()
                Then Result is 2
                """);
        }
    }

    public class GivenDifferentResponse : WhenCallAsyncMethodTwice
    {
        public GivenDifferentResponse()
            => Given<IMyService>().That(_ => _.GetValueAsync()).First().Returns(() => 1).AndNext().Returns(() => 2);

        [Fact]
        public void ThenReturnDifferentResponses()
        {
            Then().Result.Is(3);
            Specification.Is(
                """
                Given IMyService.GetValueAsync() first returns 1
                  and next returns 2
                When async _ => await _.GetServiceValueAsync() + await _.GetServiceValueAsync()
                Then Result is 3
                """);
        }
    }

    public class GivenThrowsSecondTime : WhenCallAsyncMethodTwice
    {
        public GivenThrowsSecondTime()
            => Given<IMyService>().That(_ => _.GetValueAsync()).First().Returns(() => 1).AndNext().Throws(An<ArgumentException>);

        [Fact]
        public void ThenThrows()
        {
            Then().Throws(The<ArgumentException>);
            Specification.Is(
                """
                Given IMyService.GetValueAsync() first returns 1
                  and next throws an ArgumentException
                When async _ => await _.GetServiceValueAsync() + await _.GetServiceValueAsync()
                Then throws the ArgumentException
                """);
        }
    }
}