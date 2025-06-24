using XspecT.Assert;

namespace XspecT.Test.Tests.Delay;

public abstract class WhenGetStateAfterSetStateWithAfterDelay : Spec<DelayedState, int>
{
    private static readonly Tag<int> delay = new(), state = new(), wait = new();

    protected WhenGetStateAfterSetStateWithAfterDelay()
        => Given().Default(() => The(delay))
        .When(_ => _.State)
        .After(_ => _.SetState(The(state)), () => The(wait));

    public class GivenZeroDelay : WhenGetStateAfterSetStateWithAfterDelay
    {
        public GivenZeroDelay() => Given().The(delay, 0);
        [Fact] public void ThenGetNewState() => Result.Is(The(state));
    }

    public class GivenWaitShorterThanDelay : WhenGetStateAfterSetStateWithAfterDelay
    {
        public GivenWaitShorterThanDelay() => Given().The(delay, 200).And().The(wait, 100);
        [Fact] public void ThenGetInitialState() => Result.Is(0);
    }

    public class GivenWaitLongerThanDelay : WhenGetStateAfterSetStateWithAfterDelay
    {
        public GivenWaitLongerThanDelay() => Given().The(delay, 100).And().The(wait, 200);
        [Fact]
        public void ThenGetNewState()
        {
            Result.Is(The(state));
            Specification.Is(
                """
                Given the wait is 200
                  and the delay is 100
                  and the delay is default
                When _.State
                After wait () => The(wait) ms
                After _.SetState(the state)
                Then Result is the state
                """);
        }
    }
}

public abstract class WhenGetStateAfterSetStateWithAsyncTaskDelay : Spec<DelayedState, int>
{
    private static readonly Tag<int> delay = new(), state = new(), wait = new();

    protected WhenGetStateAfterSetStateWithAsyncTaskDelay()
        => Given().Default(() => The(delay))
        .When(_ => _.State)
        .After(async _ =>
        {
            _.SetState(The(state));
            await Task.Delay(The(wait));
        });

    public class GivenZeroDelay : WhenGetStateAfterSetStateWithAsyncTaskDelay
    {
        public GivenZeroDelay() => Given().The(delay, 0);
        [Fact] public void ThenGetNewState() => Result.Is(The(state));
    }

    public class GivenWaitShorterThanDelay : WhenGetStateAfterSetStateWithAsyncTaskDelay
    {
        public GivenWaitShorterThanDelay() => Given().The(delay, 200).And().The(wait, 100);
        [Fact] public void ThenGetInitialState() => Result.Is(0);
    }

    public class GivenWaitLongerThanDelay : WhenGetStateAfterSetStateWithAsyncTaskDelay
    {
        public GivenWaitLongerThanDelay() => Given().The(delay, 100).And().The(wait, 200);
        [Fact] public void ThenGetNewState() => Result.Is(The(state));
    }
}