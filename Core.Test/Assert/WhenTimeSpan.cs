using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenTimeSpan : Spec<TimeSpan>
{
    [Fact] public void IsSame() => When(_ => A(_)).Then().Result.Is(The<TimeSpan>());
    [Fact]
    public void IsNot()
        => When(_ => A(_)).Then().Result.Is().Not(Another<TimeSpan>());
    [Fact]
    public void IsLessThanEtc()
        => When(_ => A(_))
        .Then().Result.Is().LessThan(2 * The<TimeSpan>())
        .And.GreaterThan(The<TimeSpan>() / 2)
        .And.NotLessThan(The<TimeSpan>())
        .And.NotGreaterThan(The<TimeSpan>());
}