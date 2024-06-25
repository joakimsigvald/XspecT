using XspecT.Assert;
using XspecT.Test.AutoFixture;

namespace XspecT.Test.AutoMock;

public class WhenGivenUsingAndDefaultValue : Spec<MyWrapper<string>, (string, string)>
{
    [Fact]
    public void ThenApplyBothAsValues()
        => Given().Default("ABC").And().Using("DEF")
        .When(_ => _.GetValues(A<string>()))
        .Then().Result.Is(("DEF", "ABC"));

    [Fact]
    public void ThenApplyBothAsLambdas()
        => Given().Default(() => "ABC").And().Using(() => "DEF")
        .When(_ => _.GetValues(A<string>()))
        .Then().Result.Is(("DEF", "ABC"));
}

public class WhenGivenUsingAndDefaultModel : Spec<MyWrapper<MyModel>, (MyModel, MyModel)>
{
    private MyModel _first = new() { Id = 1};
    private MyModel _second = new() { Id = 2 };

    [Fact]
    public void ThenApplyBothAsValues()
        => Given().Default(_first).And().Using(_second)
        .When(_ => _.GetValues(A<MyModel>()))
        .Then().Result.Is((_second, _first));

    [Fact]
    public void ThenApplyBothAsLambdas()
        => Given().Default(() => _first).And().Using(() => _second)
        .When(_ => _.GetValues(A<MyModel>()))
        .Then().Result.Is((_second, _first));
}