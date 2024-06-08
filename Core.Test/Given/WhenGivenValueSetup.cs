using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenValueSetup : Spec<MyService, MyModel>
{
    [Fact]
    public void AsFirstSentence_ThenUseSetupInPipeline()
        => Given<MyModel>(_ => _.Name = A<string>())
        .When(_ => MyService.Echo(A<MyModel>()))
        .Then().Result.Name.Is(The<string>());

    [Fact]
    public void AsSecondSentence_ThenUseSetupInPipeline()
        => Given<IMyRepository>().That(_ => _.GetModel()).Returns(A<MyModel>)
        .And<MyModel>(_ => _.Name = A<string>())
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(The<string>());
}