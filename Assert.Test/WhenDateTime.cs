using XspecT.Assert;
using Xunit;

namespace XspecT.Test.Assert;

public class WhenDateTime : Spec<DateTime>
{
    [Fact] public void IsSame() => When(A).Then().Result.Is(The<DateTime>());
    [Fact]
    public void IsNot()
        => When(A).Then().Result.Is().Not(Another<DateTime>());
    [Fact]
    public void IsBeforeEtc()
        => When(A)
        .Then().Result.Is().Before(The<DateTime>().AddDays(1))
        .And.After(The<DateTime>().AddDays(-1))
        .And.NotBefore(The<DateTime>())
        .And.NotAfter(The<DateTime>());
}