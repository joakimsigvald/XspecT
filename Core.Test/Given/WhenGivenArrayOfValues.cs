namespace XspecT.Test.Given;

public class WhenGivenArrayOfValues : Spec<MyService, IEnumerable<int>>
{
    [Fact]
    public void ThenCanUseTwoValuesGivenSeparatelyFromMock()
        => When(_ => _.GetIds()).Given(Two<int>()).Then().Result.Is(Two<int>());

    [Fact]
    public void ThenDoNotUseTwoValuesGivenInDifferentSetup()
        => When(_ => _.GetIds()).Given<MyModel>(_ => _.Values = Two<int>()).Then().Result.Is().Empty();

    [Fact]
    public void WhenGetEnumerableOnModel_GivenTwoValues_ThenReturnTwoValues()
        => When(_ => _.GetModel().Values)
        .Given<MyModel>(_ => _.Values = Two<int>())
        .Then().Result.Is(Two<int>());

    [Fact]
    public void GivenModelHasSomeValues_AndGivenOneValue_ThenModelHasOneValue()
        => When(_ => _.GetModel().Values)
        .Given<MyModel>(_ => _.Values = Some<int>())
        .And(One<int>())
        .Then().Result.Is(One<int>());

    [Fact]
    public void GivenModelHasSomeValues_AndGivenZeroValues_ThenModelHasZeroValues()
        => When(_ => _.GetModel().Values)
        .Given<MyModel>(_ => _.Values = Some<int>())
        .And(Zero<int>())
        .Then().Result.Is(Zero<int>());
}