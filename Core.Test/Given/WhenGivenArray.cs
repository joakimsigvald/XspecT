using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.Given;

public class WhenGivenArray : SubjectSpec<MyService, MyModel>
{
    [Fact]
    public void GivenMentionOfAnyArray_FollowedBy_MentionOfArrayOfSpecificLength_ThenGetArrayOfSpecificLength()
        => Given<IMyRepository>().That(_ => _.GetModels()).Returns(Two<MyModel>)
        //.Given(Two<MyModel>)
        .When(_ => _.GetModels())
        .Then().Result.Is(Two<MyModel>());
}