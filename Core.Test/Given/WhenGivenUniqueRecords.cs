﻿using XspecT.Assert;
using XspecT.Test.TestData;

namespace XspecT.Test.Given;

public class WhenGivenUniqueRecords : Spec<MyRecord[]>
{
    [Fact]
    public void WithEnoughValueSpace_ThenGenerateUniqueModelArray()
    {
        int range = 10;
        When(_ => Five<MyRecord>()).Given().Default<int>(i => i % range).And().Default("Abc")
            .And().Unique<MyRecord>()
            .Then().Result.Is().Distinct()
            .And.Has().All(m => m.Id >= 0 && m.Id < range);
        Specification.Is(
            """
            Given int is i % range
              and "Abc" is default
              and all MyRecord are unique
            When five MyRecord
            Then Result is distinct
                and has all m.Id >= 0 && m.Id < range
            """);
    }

    [Fact]
    public void WithNotEnoughValueSpace_ThenThrowSetupFailed()
    {
        int range = 4;
        Xunit.Assert.Throws<SetupFailed>(() => 
        When(_ => Five<MyRecord>()).Given().Default<int>(i => i % range).And().Default("Abc")
            .And().Unique<MyRecord>()
            .Then().Result.Is().Distinct()
            .And.Has().All(m => m.Id >= 0 && m.Id < range));
    }
}