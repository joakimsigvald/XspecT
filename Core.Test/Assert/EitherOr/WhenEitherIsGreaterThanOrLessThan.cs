using XspecT.Assert;

namespace XspecT.Test.Assert.EitherOr;

public class WhenEitherIsGreaterThanOrLessThan : Spec<int>
{
    [Fact]
    public void AndBothIsTrue()
    {
        When(_ => 3)
        .Then().Result.Is().Either
        .GreaterThan(1).Or.LessThan(5);
        Specification.Is("""
            When 3
            Then Result is either greater than 1
                or less than 5
            """);
    }

    [Fact]
    public void AndNoneIsTrue()
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(
            () => When(_ => 7)
        .Then().Result.Is().Either
        .GreaterThan(10).Or.LessThan(5));
        ex.Message.Is("""
            When 7
            Then Result is either greater than 10
                or less than 5
            """);
        ex.InnerException.Message.Is("Expected Result to be greater than 10 but found 7");
    }

    [Fact]
    public void AndContinueWithAnd_ThenThrowsSetupFailed()
    {
        Xunit.Assert.Throws<SetupFailed>(
            () => When(_ => 7)
        .Then().Result.Is().Either
        .GreaterThan(10).And.LessThan(5));
    }
}
public class WhenEitherWithoutOr : Spec<int>
{
    [Fact]
    public void AndIsTrue()
    {
        When(_ => 3)
        .Then().Result.Is().Either
        .GreaterThan(1);
        Specification.Is("""
            When 3
            Then Result is either greater than 1
            """);
    }

    [Fact]
    public void AndIsFalse_ThenDoesNotThrow()
    {
        When(_ => 3)
        .Then().Result.Is().Either
        .GreaterThan(10);
        Specification.Is("""
            When 3
            Then Result is either greater than 10
            """);
    }
}