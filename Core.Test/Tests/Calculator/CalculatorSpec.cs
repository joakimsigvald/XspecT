using XspecT.Fixture;

using static XspecT.Test.Subjects.Calculator;
using XspecT.Verification;

namespace XspecT.Test.Tests.Calculator;

public class CalculatorSpec : StaticSpec<int>
{
    [Fact] public void WhenAddZeros_ThenSumIsZero() => When<int, int>(Add).Then().Result.Is(0);

    [Fact] public void WhenAdd_1_and_2_ThenSumIs_3() => Given(1, 2).When(Add).Then().Result.Is(3);

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(3, 4, 7)]
    public void WhenAdd_ThenReturnSum(int x, int y, int sum)
        => When<int, int>(Add).Given(x, y).Then().Result.Is(sum);

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(3, 4, 12)]
    public void WhenMultiply_ThenReturnProduct(int x, int y, int product)
        => When(() => Multiply(x, y)).Then().Result.Is(product);
}