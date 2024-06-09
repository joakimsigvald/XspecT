using XspecT.Assert;

namespace XspecT.Test.AutoFixture;

public class WhenLabeledValueReference : Spec<int> {
    public void Then_DifferentLabels_Reference_DifferentValues_And_SameLabelSameValue() 
        => When(_ => The<int>("x") + The<int>("y"))
        .Then().Result.Is(The<int>("x") + The<int>("y"))
        .And(this).The<int>("x").Is().Not(The<int>("y"));
}