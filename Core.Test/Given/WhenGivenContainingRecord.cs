﻿using XspecT.Assert;
using static XspecT.Test.Given.WhenGivenContainingRecord;

namespace XspecT.Test.Given;

public class WhenGivenContainingRecord : Spec<MyService, ContainingRecord>
{
    [Fact]
    public void GivenDefaultContainedInAnotherRecord_ThenUseDefault()
    {
        Given<MyRecord>(_ => _ with { Name = A<string>() })
            .When(_ => MyService.Echo(A<ContainingRecord>()))
            .Then().Result.MyRecord.Name.Is(The<string>());
    }

    public record ContainingRecord(MyRecord MyRecord);
}