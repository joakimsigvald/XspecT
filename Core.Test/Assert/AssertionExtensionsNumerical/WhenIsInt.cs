using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.AssertionExtensionsNumerical;

public class WhenIsInt : Spec<int>
{
    [Fact] public void GivenSame_ThenDoesNotThrow() => When(_ => _.Is(_)).Then();

    [Fact] public void GivenFail_ThenGetException() 
    {
        var ex = Xunit.Assert.Throws<XunitException>(() => Given(1).When(_ => _).Then().Result.Is(2));
        ex.InnerException.Message.Is("Expected Result to be 2 but found 1");
    }
}
