using XspecT.Assert;

namespace XspecT.Test.AutoFixture;

public class GivenTwoElementsWithIndexedTransform : WhenList
{
    public GivenTwoElementsWithIndexedTransform()
        => Given().Two<MyModel>((_, i) => _ with { Name = $"X{i + 1}" });

    [Fact] public void ThenSecondElementIsTransformed() => Then().Result.Last().Name.Is("X2");
}