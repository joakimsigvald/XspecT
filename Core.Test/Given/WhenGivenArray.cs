using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGivenArray : SubjectSpec<MyService, MyModel[]>
{
    [Fact]
    public void GivenMentionOfAnyArray_FollowedBy_MentionOfArrayOfSpecificLength_ThenGetArrayOfSpecificLength()
        => Given<IMyRepository>().That(_ => _.GetModels()).Returns(A<MyModel[]>)
        .And(Two<MyModel>)
        .And<MyModel>(_ => _.Name = A<string>())
        .When(_ => _.GetModels())
        .Then().Result.Is(Two<MyModel>()).And(Result).First().Name.Is(The<string>());
}