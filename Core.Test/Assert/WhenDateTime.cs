using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenDateTime : Spec<DateTime>
{
    [Fact] public void IsSame() => When(_ => A(_)).Then().Result.Is(The<DateTime>());
    [Fact]
    public void IsNot()
        => When(_ => A(_)).Then().Result.Is().Not(Another<DateTime>());
    [Fact]
    public void IsBeforeEtc()
        => When(_ => A(_))
        .Then().Result.Is().Before(The<DateTime>().AddDays(1))
        .And.After(The<DateTime>().AddDays(-1))
        .And.NotBefore(The<DateTime>())
        .And.NotAfter(The<DateTime>());
}