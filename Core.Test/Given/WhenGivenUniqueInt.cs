using XspecT.Assert;

namespace XspecT.Test.Given;

public class WhenGivenUniqueInt : Spec<int[]>
{
    [Fact]
    public void ThenGenerateUniqueIntArray()
    {
        int range = 10;
        When(_ => Five<int>()).Given().Default<int>(i => i % range).And().Unique<int>()
            .Then().Result.Is().Distinct()
            .And.Has().All(i => i >= 0 && i < range);
        Specification.Is(
            """
            Given int is i % range
              and all int are unique
            When five int
            Then Result is distinct
                and has all i >= 0 && i < range
            """);
    }

    [Fact]
    public void ThenGenerateUniqueIntValues()
    {
        int range = 10;
        When(_ => Five<int>(i => i % range))
            .Given().Unique<int>()
            .Then().Result.Is().Distinct()
            .And.Has().All(i => i >= 0 && i < range);
        Specification.Is(
            """
            Given all int are unique
            When five int { i % range }
            Then Result is distinct
                and has all i >= 0 && i < range
            """);
    }

    [Fact]
    public void ThenGenerateUniqueOtherIntValues()
    {
        int range = 10;
        When(_ => [
            Another<int>(),
            Another<int>(),
            Another<int>(),
            Another<int>(),
            Another<int>()])
            .Given().Default<int>(i => i % range).And().Unique<int>()
            .Then().Result.Is().Distinct()
            .And.Has().All(i => i >= 0 && i < range);
        Specification.Is(
            """
            Given int is i % range
              and all int are unique
            When [Another<int>(), Another<int>(), Another<int>(), Another<int>(),
                  Another<int>()]
            Then Result is distinct
                and has all i >= 0 && i < range
            """);
    }
}