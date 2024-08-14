namespace XspecT.Test.Given;

public class WhenGivenArrayOfValues : Spec<MyService, IEnumerable<int>>
{
    [Fact]
    public void ThenCanUseTwoValuesGivenSeparatelyFromMock()
    {
        When(_ => _.GetIds()).Given(Two<int>()).Then().Result.Is(Two<int>());
        Description.Is(
            """
            Given two int
            When GetIds()
            Then Result is two int
            """);
    }

    [Fact]
    public void ThenDoNotUseTwoValuesGivenInDifferentSetup()
    {
        When(_ => _.GetIds()).Given<MyModel>(_ => _.Values = Two<int>()).Then().Result.Is().Empty();
        Description.Is(
            """
            Given MyModel { Values = two int }
            When GetIds()
            Then Result is empty
            """);
    }

    [Fact]
    public void WhenGetEnumerableOnModel_GivenTwoValues_ThenReturnTwoValues()
    {
        When(_ => _.GetModel().Values)
            .Given<MyModel>(_ => _.Values = Two<int>())
            .Then().Result.Is(Two<int>());
        Description.Is(
            """
            Given MyModel { Values = two int }
            When GetModel().Values
            Then Result is two int
            """);
    }

    [Fact]
    public void GivenModelHasSomeValues_AndGivenOneValue_ThenModelHasOneValue()
    {
        When(_ => _.GetModel().Values)
            .Given<MyModel>(_ => _.Values = Some<int>())
            .And(One<int>())
            .Then().Result.Is(One<int>());
        Description.Is(
            """
            Given MyModel { Values = some int }
             and one int
            When GetModel().Values
            Then Result is one int
            """);
    }

    [Fact]
    public void GivenModelHasSomeValues_AndGivenZeroValues_ThenModelHasZeroValues()
    {
        When(_ => _.GetModel().Values)
            .Given<MyModel>(_ => _.Values = Some<int>())
            .And(Zero<int>())
            .Then().Result.Is(Zero<int>());
        Description.Is(
            """
            Given MyModel { Values = some int }
             and zero int
            When GetModel().Values
            Then Result is zero int
            """);
    }
}