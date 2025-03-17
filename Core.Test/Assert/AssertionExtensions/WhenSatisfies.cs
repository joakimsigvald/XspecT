using XspecT.Assert;
using XspecT.Test.Assert.Continuations.IsObject;

namespace XspecT.Test.Assert.AssertionExtensions;

public class WhenSatisfies : Spec
{
    [Fact]
    public void GivenTrue_ThenDoesNotThrow()
    {
        MyRecord actual = new("Abc");
        actual.Satisfies(_ => _.Name == "Abc").And.Satisfies(_ => _.Name == "Abc").And.Is().Not().Null();
    }

    [Fact]
    public void GivenFalse_ThenGetException()
    {
        MyRecord myRecord = new("XXX");
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => myRecord.Satisfies(_ => _.Name == "Abc"));
        ex.Message.Is("MyRecord satisfies _.Name == \"Abc\"");
        ex.InnerException.Message.Is($"Expected myRecord to satisfy _.Name == \"Abc\" but found {myRecord}");
    }
}