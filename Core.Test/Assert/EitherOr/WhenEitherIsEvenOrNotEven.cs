using XspecT.Assert;

namespace XspecT.Test.Assert.EitherOr;

public class WhenEitherIsEvenOrNotEven : Spec<int>
{
    [Fact]
    public void AndSecondIsTrue()
    {
        When(_ => 1).Then().Result.Is().Either.Even().Or.Not().Even();
        Specification.Is("""
            When 1
            Then Result is either even
                or not even
            """);
    }

    [Fact]
    public void AndFirstIsTrue()
    {
        When(_ => 2).Then().Result.Is().Either.Even().Or.Not().Even();
        Specification.Is("""
            When 2
            Then Result is either even
                or not even
            """);
    }
}