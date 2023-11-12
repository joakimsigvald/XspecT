using XspecT.Assert;
using XspecT.Test.Given;

namespace XspecT.Test.When;

public class WhenDo_A_AndThen_B : SubjectSpec<MyService, int>
{
    private int _nextId = 0;

    [Fact]
    public void ThenCall_A_Before_B()
        => Given<IMyRepository>().That(_ => _.GetNextId()).Returns(() => ++_nextId)
        .When(_ => _.GetNextId()).And(_ => _.GetNextId())
        .Then().Result.Is(2);
}