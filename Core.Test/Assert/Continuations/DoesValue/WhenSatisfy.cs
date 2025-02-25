using XspecT.Assert;
using XspecT.Test.Assert.Continuations.IsObject;

namespace XspecT.Test.Assert.Continuations.DoesValue;

public class WhenSatisfy : Spec
{
    [Fact]
    public void GivenTrue_ThenDoesNotThrow()
    {
        MyRecord actual = new("Abc");
        actual.Does().Satisfy(_ => _.Name == "Abc").And.Satisfies(_ => _.Name == "Abc").And.Is().NotNull();
    }

    [Fact]
    public void GivenFalse_ThenGetException()
    {
        MyRecord myRecord = new("XXX");
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => myRecord.Does().Satisfy(_ => _.Name == "Abc"));
        ex.Message.Is("MyRecord satisfies _.Name == \"Abc\"");
        ex.InnerException.Message.Is($"Expected myRecord to satisfy _.Name == \"Abc\" but found {myRecord}");
    }
}