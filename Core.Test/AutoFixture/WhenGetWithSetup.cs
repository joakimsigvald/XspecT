using XspecT.Fixture;
using XspecT.Fixture.Exceptions;
using XspecT.Verification;

namespace XspecT.Test.AutoFixture;

public class WhenGetWithSetup : SubjectSpec<MyMappingRetreiver, MyModel>
{
    public WhenGetWithSetup() 
        => When(_ => _.Get(An<int>()))
        .Given<IMyRepository>().That(_ => _.Get(The<int>())).Returns(() => A<MyModel>(_ => _.Name = A<string>()));

    [Fact]
    public void Setup_CanBeProvided_ToPreviouslyMentionedModel()
        => Given<IMyMapper>().That(_ => _.Map(The<MyModel>())).Returns(The<MyModel>)
        .Then().Result.Name.Is(The<string>());

    [Fact]
    public void Setup_CanBeProvided_MoreThanOnce_ToSameModel()
        => Given<IMyMapper>().That(_ => _.Map(The<MyModel>()))
        .Returns(() => A<MyModel>(_ => _.Id = An<int>()))
        .Then().Result.Name.Is(The<string>()).And(Result).Id.Is(The<int>());

    [Fact]
    public void Setup_CannotBeProvided_AfterThen()
        => Assert.Throws<SetupFailed>(
            () => Given<IMyMapper>().That(_ => _.Map(A<MyModel>())).Returns(The<MyModel>)
            .Then().Result.Is(A<MyModel>(_ => _.Name = "abc")));
}