﻿using XspecT.Assert;

namespace XspecT.Test.AutoMock;

public class WhenMockFuncOfDateTime : Spec<DateService, DateTime>
{
    public WhenMockFuncOfDateTime() => When(_ => _.GetNow());

    public class GivenItWasNotProvided : WhenMockFuncOfDateTime
    {
        [Fact] 
        public void Then_It_Has_RandomDateTime() 
            => Then().Result.Is().Not(The<DateTime>()).And(Result).Ticks.Is().Not(0);
    }

    public class GivenItWasProvided : WhenMockFuncOfDateTime
    {
        [Fact] public void Then_It_Has_ProvidedValue() => Given(A<DateTime>()).Then().Result.Is(The<DateTime>());
    }
}

public class DateService(Func<DateTime> now)
{
    private readonly Func<DateTime> _now = now;
    public DateTime GetNow() => _now();
}