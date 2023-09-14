using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGivenValue : SubjectSpec<MyService, MyModel>
{
    [Fact]
    public void AsFirstSentence_ThenValueInPipeline()
        => Given(new MyModel() { Name = A<string>() })
        .When(_ => MyService.Echo(The<MyModel>()))
        .Then().Result.Name.Is(The<string>());

    [Fact]
    public void AsSecondSentence_ThenUseValueInPipeline()
        => Given<IMyRepository>().That(_ => _.GetModel()).Returns(A<MyModel>)
        .And(new MyModel() { Name = A<string>() })
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(The<string>());

    [Fact]
    public void AsReturnsValue_ThenUseValueInPipeline()
        => Given<IMyRepository>().That(_ => _.GetModel()).Returns(() => A(new MyModel() { Name = A<string>() }))
        .When(_ => _.GetModel())
        .Then().Result.Name.Is(The<string>());
}