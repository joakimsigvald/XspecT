using XspecT.Fixture;
using XspecT.Verification;

namespace XspecT.Test.AutoMock;

public class WhenMockDateTime : SubjectSpec<StaticDateService, DateTime>
{
    public WhenMockDateTime() => When(_ => _.GetDate());
    public class GivenItWasNotProvided : WhenMockDateTime
    {
        [Fact] public void Then_It_Has_FirstDateTime() => Then().Result.Is(A<DateTime>());
    }

    public class GivenItWasProvided : WhenMockDateTime
    {
        [Fact]
        public void Then_It_Has_ProvidedValue()
            => Using(A<DateTime>()).Then().Result.Is(The<DateTime>());
    }
}

public class StaticDateService
{
    private readonly DateTime _date;
    public StaticDateService(DateTime date) => _date = date;
    public DateTime GetDate() => _date;
}