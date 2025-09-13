using XspecT.Test.TestData;

namespace XspecT.Test.Given;

public class GivenDefaultEnumerableNotProvidedWhenGetTaskOfIEnumerable : Spec<MyService, MyModel[]>
{
    [Fact]
    public void GivenDefaultEnumerableNotProvided_WhenGetTaskOfEnumerable_ThrowSetupFailed()
    {
        Xunit.Assert.Throws<SetupFailed>(() =>
        When(_ => _.GetModelsAsync()).Then().DoesNotThrow());
    }
}