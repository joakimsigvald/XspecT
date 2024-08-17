using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenArrayOfModels : Spec<MyService, MyModel[]>
{
    [Fact]
    public void GivenMentionOfAnyArray_FollowedBy_MentionOfArrayOfSpecificLength_ThenGetArrayOfSpecificLength()
    {
        Given<IMyRepository>().That(_ => _.GetModels()).Returns(() => A<MyModel[]>())
            .And(Two<MyModel>)
            .And<MyModel>(_ => _.Name = A<string>())
            .When(_ => _.GetModels())
            .Then().Result.Is(Two<MyModel>()).And(Result).First().Name.Is(The<string>());
        Specification.Is(
            """
            Given MyModel { Name = a string }
              and two MyModel
              and IMyRepository.GetModels() returns a MyModel[]
            When _.GetModels()
            Then Result is two MyModel
              and Result's First().Name is the string
            """);
    }

    [Fact]
    public void GivenDefaultEnumerableProvided_CanGetTaskOfEnumerable()
    {
        When(_ => _.GetModelsAsync())
            .Given<IMyRepository>().Returns(An<IEnumerable<MyModel>>)
            .Then().DoesNotThrow();
        Specification.Is(
            """
            Given IMyRepository returns an IEnumerable<MyModel>
            When _.GetModelsAsync()
            Then does not throw
            """);
    }

    [Fact]
    public void GivenDefaultEnumerableNotProvided_WhenGetTaskOfEnumerable_ThrowSetupFailed()
    {
        Xunit.Assert.Throws<SetupFailed>(() =>
        When(_ => _.GetModelsAsync()).Then().DoesNotThrow());
    }
}