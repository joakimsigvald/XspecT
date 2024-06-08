using XspecT.Assert;
using Xunit;

namespace XspecT.Test.Assert;

public class WhenDateTime : SubjectSpec<object, DateTime>
{
    [Fact] public void IsSame() => When(_ => A<DateTime>()).Then().Result.Is(The<DateTime>());
    [Fact]
    public void IsNot()
        => When(_ => A<DateTime>()).Then().Result.Is().Not(Another<DateTime>());
    [Fact]
    public void IsBeforeEtc()
        => When(_ => A<DateTime>())
        .Then().Result.Is().Before(The<DateTime>().AddDays(1))
        .And.After(The<DateTime>().AddDays(-1))
        .And.NotBefore(The<DateTime>())
        .And.NotAfter(The<DateTime>());
}