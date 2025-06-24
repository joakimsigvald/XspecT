using XspecT.Assert;
using XspecT.Test.AutoFixture;

namespace XspecT.Test.AutoMock;

public class WhenGivenUsingAndDefaultValue : Spec<MyWrapper<string>, (string, string)>
{
    [Fact]
    public void ThenApplyBothAsValues()
    {
        Given().Default("ABC").And().Using("DEF")
            .When(_ => _.GetValues(A<string>()))
            .Then().Result.Is(("DEF", "ABC"));
        Specification.Is(
            """
            Given "ABC" is default
              and using "DEF"
            When _.GetValues(a string)
            Then Result is ("DEF", "ABC")
            """);
    }

    [Fact]
    public void ThenApplyBothAsLambdas()
    {
        Given().Default(() => "ABC").And().Using(() => "DEF")
            .When(_ => _.GetValues(A<string>()))
            .Then().Result.Is(("DEF", "ABC"));
        Specification.Is(
            """
            Given using "DEF"
              and "ABC" is default
            When _.GetValues(a string)
            Then Result is ("DEF", "ABC")
            """);
    }
}

public class WhenGivenUsingAndDefaultModel : Spec<MyWrapper<MyModel>, (MyModel, MyModel)>
{
    private readonly MyModel _first = new() { Id = 1};
    private readonly MyModel _second = new() { Id = 2 };

    [Fact]
    public void ThenApplyBothAsValues()
    {
        Given().Default(_first).And().Using(_second)
            .When(_ => _.GetValues(A<MyModel>()))
            .Then().Result.Is((_second, _first));
        Specification.Is(
            """
            Given _first is default
              and using _second
            When _.GetValues(a MyModel)
            Then Result is (_second, _first)
            """);
    }

    [Fact]
    public void ThenApplyBothAsLambdas()
    {
        Given().Default(() => _first).And().Using(() => _second)
            .When(_ => _.GetValues(A<MyModel>()))
            .Then().Result.Is((_second, _first));
        Specification.Is(
            """
            Given using _second
              and _first is default
            When _.GetValues(a MyModel)
            Then Result is (_second, _first)
            """);
    }
}