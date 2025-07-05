using XspecT.Assert;
using XspecT.Test.Assert.Continuations.IsObject;

namespace XspecT.Test.Assert.AssertionExtensions;

public class WhenSatisfies : Spec
{
    [Fact]
    public void GivenTrue_ThenDoesNotThrow()
    {
        MyRecord actual = new("Abc");
        actual.Has(_ => _.Name == "Abc").And.Has(_ => _.Name == "Abc").And.Is().Not().Null();
    }

    [Fact]
    public void GivenFalse_ThenGetException()
    {
        MyRecord myRecord = new("XXX");
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => myRecord.Has(_ => _.Name == "Abc"));
        ex.Message.Is("MyRecord has _.Name == \"Abc\"");
        ex.InnerException.Message.Is($"Expected myRecord to have _.Name == \"Abc\" but found {myRecord}");
    }
}