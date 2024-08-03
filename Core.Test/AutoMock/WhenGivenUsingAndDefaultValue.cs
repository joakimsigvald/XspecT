using static XspecT.Test.Helper;
using XspecT.Test.AutoFixture;

namespace XspecT.Test.AutoMock;

public class WhenGivenUsingAndDefaultValue : Spec<MyWrapper<string>, (string, string)>
{
    [Fact]//TODO
    public void ThenApplyBothAsValues()
    {
        Given().Default("ABC").And().Using("DEF")
            .When(_ => _.GetValues(A<string>()))
            .Then().Result.Is(("DEF", "ABC"));
        VerifyDescription(
            """
            Given default "ABC"
             given using "DEF"
             when GetValues(a string),
             then Result is ("DEF", "ABC")
            """);
    }

    [Fact]
    public void ThenApplyBothAsLambdas()
        => Given().Default(() => "ABC").And().Using(() => "DEF")
        .When(_ => _.GetValues(A<string>()))
        .Then().Result.Is(("DEF", "ABC"));
}

public class WhenGivenUsingAndDefaultModel : Spec<MyWrapper<MyModel>, (MyModel, MyModel)>
{
    private readonly MyModel _first = new() { Id = 1};
    private readonly MyModel _second = new() { Id = 2 };

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