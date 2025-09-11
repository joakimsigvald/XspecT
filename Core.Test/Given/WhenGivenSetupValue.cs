using XspecT.Assert;
using XspecT.Test.Given.TestData;

namespace XspecT.Test.Given;

public class WhenGivenSetupValue : Spec<MyService, DateTime>
{
    private static readonly DateTime _now = DateTime.Now;
    private static readonly DateTime _anotherTime = DateTime.Now.AddDays(1);

    [Fact]
    public void ThenCanApplySpecificValueForPreviouslyMentionedType()
    {
        Given(A<DateTime>)
            .When(_ => _.GetTime())
            .Given().A(_now)
            .Then().Result.Is(_now);
        Specification.Is(
            """
            Given a DateTime is _now
              and a DateTime
            When _.GetTime()
            Then Result is _now
            """);
    }

    [Fact]
    public void ThenApplyFirstSpecifiedValueForPreviouslyMentionedType()
    {
        Given(A<DateTime>)
            .When(_ => _.GetTime())
            .Given().A(_now)
            .And().A(_anotherTime) //Ignore this since a specific value has already been provided
            .Then().Result.Is(_now);
        Specification.Is(
            """
            Given a DateTime is _anotherTime
              and a DateTime is _now
              and a DateTime
            When _.GetTime()
            Then Result is _now
            """);
    }
}