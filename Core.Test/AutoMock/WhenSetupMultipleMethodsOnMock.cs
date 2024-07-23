using XspecT.Assert;
using XspecT.Test.Given;

namespace XspecT.Test.AutoMock;

public class WhenSetupMultipleMethodsOnMock : Spec<MyValueIntService, string>
{
    [Fact]
    public void ThenSetupAll()
        => When(_ => _.GetValue(A<MyValueInt>()))
        .Given<IMyValueIntRepo>()
        .That(_ => _.GetAsync(The<MyValueInt>())).Returns(() => A<string>())
        .AndThat(_ => _.Get(The<MyValueInt>())).Returns(() => ASecond<string>())
        .Then().Result.Is(TheSecond<string>());
}

public class WhenMockReturnTaskOfInterface : Spec<MyValueIntService, IMyValueIntRepo>
{
    [Fact]
    public void ThenThrowSetupFailed()
    {
        var error = Xunit.Assert.Throws<SetupFailed>(
            () => When(_ => _.GetRepoAsync()).Then().Result.Get(1).Is().NotNull());
        Xunit.Assert.Contains("Given<IMyValueIntRepo>().Returns(A<IMyValueIntRepo>)", error.Message);
    }
}