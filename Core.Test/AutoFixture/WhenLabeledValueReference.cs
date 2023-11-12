using XspecT.Assert;

namespace XspecT.Test.AutoFixture;

public class WhenLabeledValueReference : StaticSpec<int> {
    public void Then_DifferentLabels_Reference_DifferentValues_And_SameLabelSameValue() 
        => Given(The<int>("x"), The<int>("y"))
        .When((x, y) => x + y)
        .Then().Result.Is(The<int>("x") + The<int>("y"))
        .And(this).The<int>("x").Is().Not(The<int>("y"));
}