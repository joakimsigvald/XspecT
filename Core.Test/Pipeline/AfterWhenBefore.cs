namespace XspecT.Test.Pipeline;

public class AfterWhenBefore : Spec<MyStateService, int>
{
    [Fact]
    public void BeforeIsExecutedAfterWhen()
    {
        When(AdvanceCounter).Before(_ => _.Counter--).Then().Result.Is(1);
        Description.Is(
            """
            When AdvanceCounter
            Before _.Counter--
            Then Result is 1
            """);
    }

    [Fact]
    public void AfterIsExecutedBeforeWhen()
        => When(_ => AdvanceCounter(_)).After(_ => _.Counter++).Then().Result.Is(2);

    [Fact]
    public void FirstAfterIsExecutedAfterSecondAfterBeforeWhen()
        => When(_ => DoubleCounter(_))
        .After(_ => _.Counter = 3)
        .After(_ => _.Counter = 5)
        .Then().Result.Is(6);

    [Fact]
    public void GivenWhenExecutedTwice_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(
            () => When(_ => AdvanceCounter(_)).When(_ => DoubleCounter(_)));

    [Fact]
    public void GivenBeforeExecutedTwice_BothAreExecuted()
        => When(_ => SetCounter(_, 1)).Before(_ => _.Counter = 3).Before(_ => _.Counter = 2)
        .Then().Result.Is(1);

    [Fact]
    public void GivenCallThenBeforeWhen_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(() => Then().Throws<Exception>());

    [Fact]
    public void WhenBeforeGivenAfter()
        => When(_ => AdvanceCounter(_)).Before(_ => ++_.Counter).Given(1).After(_ => _.Counter++).Then().Result.Is(2);

    private static int AdvanceCounter(MyStateService service) => ++service.Counter;

    private static int DoubleCounter(MyStateService service)
        => service.Counter *= 2;

    private static int SetCounter(MyStateService service, int val)
        => service.Counter = val;
}