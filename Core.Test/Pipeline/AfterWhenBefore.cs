using XspecT.Assert;

namespace XspecT.Test.Pipeline;

public class AfterWhenBefore : Spec<MyStateService, int>
{
    [Fact]
    public void BeforeIsExecutedAfterWhen()
        => When(_ => AdvanceCounter(_)).Before(_ => _.Counter--).Then().Result.Is(1);

    private int AdvanceCounter(MyStateService service) => ++service.Counter;

    [Fact]
    public void AfterIsExecutedBeforeWhen()
        => When(_ => AdvanceCounter(_)).After(_ => _.Counter++).Then().Result.Is(2);

    [Fact]
    public void FirstAfterIsExecutedAfterSecondAfterBeforeWhen()
        => When(_ => DoubleCounter(_))
        .After(_ => _.Counter = 3)
        .After(_ => _.Counter = 5)
        .Then().Result.Is(6);

    private int DoubleCounter(MyStateService service) 
        => service.Counter *=2;

    [Fact]
    public void GivenWhenExecutedTwice_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(() => When(_ => { ++_.Counter; }).When(_ => { _.Counter--; }));

    [Fact]
    public void GivenBeforeExecutedTwice_BothAreExecuted()
        => When(_ => SetCounter(_, 1)).Before(_ => _.Counter = 3).Before(_ => _.Counter = 2)
        .Then().Result.Is(1);

    private int SetCounter(MyStateService service, int val)
        => service.Counter = val;


    [Fact]
    public void GivenCallThenBeforeWhen_ThenThrowSetupFailed()
        => Xunit.Assert.Throws<SetupFailed>(() => Then().Throws<Exception>());

    [Fact]
    public void WhenBeforeGivenAfter()
        => When(_ => AdvanceCounter(_)).Before(_ => ++_.Counter).Given(1).After(_ => _.Counter++).Then().Result.Is(2);
}