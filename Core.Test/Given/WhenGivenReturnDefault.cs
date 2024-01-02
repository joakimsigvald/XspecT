using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenReturnDefault : SubjectSpec<MyService, MyModel>
{
    [Fact]
    public void ThenMockReturnDefault()
        => Given<IMyRepository>().ReturnsDefault(A<MyModel>)
        .When(_ => _.GetModel())
        .Then().Result.Is(The<MyModel>());
}