using XspecT.Assert;
using Xunit;

namespace XspecT.Test.Verification;

public class WhenTimeSpan : Spec<TimeSpan>
{
    [Fact] public void IsSame() => When(A).Then().Result.Is(The<TimeSpan>());
    [Fact]
    public void IsNot()
        => When(A).Then().Result.Is().Not(Another<TimeSpan>());
    [Fact]
    public void IsLessThanEtc()
        => When(A)
        .Then().Result.Is().LessThan(2 * The<TimeSpan>())
        .And.GreaterThan(The<TimeSpan>() / 2)
        .And.NotLessThan(The<TimeSpan>())
        .And.NotGreaterThan(The<TimeSpan>());
}