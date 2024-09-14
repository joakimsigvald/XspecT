using XspecT.Assert;
using XspecT.Test.Given;

namespace XspecT.Test.AutoMock;

public class WhenSetupMultipleMethodsOnMock : Spec<MyValueIntService, string>
{
    [Fact]
    public void ThenSetupAll()
    {
        When(_ => _.GetValue(A<MyValueInt>()))
            .Given<IMyValueIntRepo>()
            .That(_ => _.GetAsync(The<MyValueInt>())).Returns(A<string>)
            .AndThat(_ => _.Get(The<MyValueInt>())).Returns(ASecond<string>)
            .Then().Result.Is(TheSecond<string>());
        Specification.Is(
            """
            Given IMyValueIntRepo.GetAsync(the MyValueInt) returns a string
              and Get(the MyValueInt) returns a second string
            When _.GetValue(a MyValueInt)
            Then Result is the second string
            """);
    }
}
public class WhenCallMethodTwice : Spec<InterfaceService, int> 
{
    public WhenCallMethodTwice() 
        => When(_ => _.GetServiceValue() + _.GetServiceValue());

    [Fact]
    public void GivenOneMockedResponse_ThenReturnThatResponsTwice() 
        => Given<IMyService>()
            .That(_ => _.GetValue()).Returns(() => 1)
            .Then().Result.Is(2);

    [Fact]
    public void GivenDifferentResponse_ThenReturnDifferentResponses()
        => Given<IMyService>()
            .That(_ => _.GetValue()).First().Returns(() => 1).AndNext().Returns(() => 2)
            .Then().Result.Is(3);
}