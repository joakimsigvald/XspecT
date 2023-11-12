using XspecT.Assert;

namespace XspecT.Test.Verification;

public class WhenDateTime : StaticSpec<DateTime>
{
    [Fact] public void IsSame() => Given(A<DateTime>()).When(_ => _).Then().Result.Is(The<DateTime>());
    [Fact]
    public void IsNot()
        => Given(A<DateTime>()).When(_ => _).Then().Result.Is().Not(Another<DateTime>());
    [Fact]
    public void IsBeforeEtc()
        => Given(A<DateTime>()).When(_ => _)
        .Then().Result.Is().Before(The<DateTime>().AddDays(1))
        .And.After(The<DateTime>().AddDays(-1))
        .And.NotBefore(The<DateTime>())
        .And.NotAfter(The<DateTime>());
}