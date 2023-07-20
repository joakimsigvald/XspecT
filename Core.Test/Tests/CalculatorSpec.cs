using XspecT.Verification;
using XspecT.Fixture;
using Xunit;

using static XspecT.Test.Subjects.Calculator;

namespace XspecT.Test.Tests;

public class CalculatorSpec : StaticSpec<int>
{
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(3, 4, 7)]
    public void WhenAdd_ThenReturnSum(int x, int y, int sum)
        => When<int, int>(Add).Given(x, y).Then.Result.Is(sum);

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(3, 4, 12)]
    public void WhenMultiply_ThenReturnProduct(int x, int y, int product)
        => When(() => Multiply(x, y)).Then.Result.Is(product);
}

public abstract class WhenAdd : StaticSpec<int>
{
    public WhenAdd() => When<int, int>(Add);

    public class Given_1_1 : WhenAdd
    {
        public Given_1_1() => Given(1, 1);
        [Fact] public void Then_Return_2() => Result.Is(2);
        [Fact] public void Then_Return_Between_1_And_3() => Result.IsGreaterThan(1).And.BeLessThan(3);
    }

    public class Given_2_3 : WhenAdd
    {
        public Given_2_3() => Given(2, 3);
        [Fact] public void Then_Return_5() => Result.Is(5);
    }
}