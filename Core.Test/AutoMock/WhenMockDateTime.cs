using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public class WhenMockDateTime : Spec<StaticDateService, DateTime>
{
    public WhenMockDateTime() => When(_ => _.GetDate());
    public class GivenItWasNotProvided : WhenMockDateTime
    {
        [Fact] 
        public void Then_It_Has_RandomDateTime() 
            => Then().Result.Is().Not(A<DateTime>()).And(Result).Ticks.Is().Not(0);
    }

    public class GivenItWasProvided : WhenMockDateTime
    {
        [Fact]
        public void Then_It_Has_ProvidedValue()
            => Given(A<DateTime>()).Then().Result.Is(The<DateTime>());
    }
}

public class StaticDateService(DateTime date)
{
    private readonly DateTime _date = date;
    public DateTime GetDate() => _date;
}