﻿using XspecT.Assert;

namespace XspecT.Test.Assert;

public class WhenTimeSpan : Spec<TimeSpan>
{
    [Fact]
    public void IsSame()
    {
        When(_ => A(_)).Then().Result.Is(The<TimeSpan>());
        Specification.Is(
            """
            When a _
            Then Result is the TimeSpan
            """);
    }

    [Fact]
    public void IsNot()
    {
        When(_ => A(_)).Then().Result.Is().Not(Another<TimeSpan>());
        Specification.Is(
            """
            When a _
            Then Result is not another TimeSpan
            """);
    }

    [Fact]
    public void IsLessThanEtc()
    {
        When(_ => A(_))
            .Then().Result.Is().LessThan(2 * The<TimeSpan>())
            .And.GreaterThan(The<TimeSpan>() / 2)
            .And.NotLessThan(The<TimeSpan>())
            .And.NotGreaterThan(The<TimeSpan>());
        Specification.Is(
            """
            When a _
            Then Result is less than 2 * the TimeSpan
                and greater than the TimeSpan / 2
                and not less than the TimeSpan
                and not greater than the TimeSpan
            """);
    }
}