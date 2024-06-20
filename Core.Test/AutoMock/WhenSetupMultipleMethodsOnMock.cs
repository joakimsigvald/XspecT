using XspecT.Assert;
using XspecT.Test.Given;

namespace XspecT.Test.AutoMock;

public class WhenSetupMultipleMethodsOnMock : Spec<MyValueIntService, string>
{
    [Fact]
    public void ThenSetupAll()
        => When(_ => _.GetValue(A<MyValueInt>()))
        .Given<IMyValueIntRepo>()
        .That(_ => _.GetAsync(The<MyValueInt>())).Returns(A<string>)
        .AndThat(_ => _.Get(The<MyValueInt>())).Returns(ASecond<string>)
        .Then().Result.Is(TheSecond<string>());
}