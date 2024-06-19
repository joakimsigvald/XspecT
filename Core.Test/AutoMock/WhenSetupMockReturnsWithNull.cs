using XspecT.Test.Given;

namespace XspecT.Test.AutoMock;

public class WhenSetupMockReturnsWithNull : Spec<MyValueIntService, string>
{
    [Fact]
    public void GivenReturns_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(() =>
        Given<IMyValueIntRepo>().That(_ => _.GetAsync(The<MyValueInt>())).Returns(null));

    [Fact]
    public void AndReturns_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(() =>
        Given<IMyValueIntRepo>().That(_ => _.GetAsync(The<MyValueInt>())).Returns(() => null)
        .And<IMyValueIntRepo>().That(_ => _.GetAsync(The<MyValueInt>())).Returns(null));
}