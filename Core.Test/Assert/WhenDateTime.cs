namespace XspecT.Test.Assert;

public class WhenDateTime : Spec<DateTime>
{
    [Fact]
    public void IsSame()
    {
        When(_ => A(_)).Then().Result.Is(The<DateTime>());
        Specification.Is(
            """
            When a _
            Then Result is the DateTime
            """);
    }

    [Fact]
    public void IsNot()
    {
        When(_ => A(_)).Then().Result.Is().Not(Another<DateTime>());
        Specification.Is(
            """
            When a _
            Then Result is not another DateTime
            """);
    }

    [Fact]
    public void IsBeforeEtc()
    {
        When(_ => A(_))
            .Then().Result.Is().Before(The<DateTime>().AddDays(1))
            .And.After(The<DateTime>().AddDays(-1))
            .And.NotBefore(The<DateTime>())
            .And.NotAfter(The<DateTime>());
        Specification.Is(
            """
            When a _
            Then Result is before the DateTime's AddDays(1)
                and after the DateTime's AddDays(-1)
                and not before the DateTime
                and not after the DateTime
            """);
    }
}