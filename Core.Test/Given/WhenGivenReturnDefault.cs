using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenReturnDefault : Spec<MyService, MyModel>
{
    [Fact]
    public void ThenMockReturnDefault()
    {
        Given<IMyRepository>().Returns(A<MyModel>)
            .When(_ => _.GetModel())
            .Then().Result.Is(The<MyModel>());
        Specification.Is(
            """
            Given IMyRepository returns a MyModel
            When _.GetModel()
            Then Result is the MyModel
            """);
    }
}