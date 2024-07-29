using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenArrayOfModels : Spec<MyService, MyModel[]>
{
    [Fact]
    public void GivenMentionOfAnyArray_FollowedBy_MentionOfArrayOfSpecificLength_ThenGetArrayOfSpecificLength()
        => Given<IMyRepository>().That(_ => _.GetModels()).Returns(A<MyModel[]>)
        .And(Two<MyModel>)
        .And<MyModel>(_ => _.Name = A<string>())
        .When(_ => _.GetModels())
        .Then().Result.Is(Two<MyModel>()).And(Result).First().Name.Is(The<string>());

    [Fact]
    public void GivenDefaultEnumerableProvided_CanGetTaskOfEnumerable()
        => When(_ => _.GetModelsAsync())
        .Given<IMyRepository>().Returns(An<IEnumerable<MyModel>>)
        .Then().DoesNotThrow();

    [Fact]
    public void GivenDefaultEnumerableNotProvided_WhenGetTaskOfEnumerable_ThrowSetupFailed()
    {
        Xunit.Assert.Throws<SetupFailed>(() =>
        When(_ => _.GetModelsAsync()).Then().DoesNotThrow());
    }
}