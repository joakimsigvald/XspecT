using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public class WhenMockReturnTaskOfInterface : Spec<MyValueIntService, IMyValueIntRepo>
{
    [Fact]
    public void ThenThrowSetupFailed()
    {
        var error = Xunit.Assert.Throws<SetupFailed>(
            () => When(_ => _.GetRepoAsync()).Then().Result.Get(1).Is().NotNull());
        error.Message.Does().Contain("Given<IMyValueIntRepo>().Returns(A<IMyValueIntRepo>)");
    }
}